using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// This class handles the fusion of stars.
/// 
/// 1. Subscribes to PlayerController.onOrbit, checking if the orbitted objects are stars, and if so, adds them to `orbittedStars`
/// 2. Once there is enough orbittedStars, it waits for the next time the player launches (that way, fusion starts after the player leaves the last required star)
/// 3. Once the player launches, FuseStars() is called, which makes the Stars start moving towards each other
/// 4. This class waits until all stars have moved together, and then destroys them.
///
/// Note that this component is destroyed and recreated frequently.
/// TODO: I don't think this class can handle more than one fusion process at a time. Fix that
/// </summary>
public class StarFuser : MonoBehaviour
{
    [SerializeField] int requiredStarsForFusion = 3;
    PlayerController player;

    #region Star lists
    // Stars that are required to be fused before the exit portal opens
    List<Star> requiredStars = new List<Star>();
    
    // Stars that the player has recently orbitted. Clears once the player reaches the required number to fuse.
    List<Star> orbittedStars = new List<Star>();

    // Stars that are currently participating in a fusion process. NOTE: only one set of stars should be included in this at a time.
    List<Star> starsToFuse = new List<Star>();
    int numOfStarsWaitingFor = 0;
    #endregion

    UnityAction fuseDelegate = null;
    UnityAction starDecrementDelegate = null;

    // true when the fuse operation has begun.
    // reset to false once fully fused.
    bool startedFuse = false;

    ExitPortal exitPortal = null;

    void Start()
    {
        player = GameObject.FindObjectOfType<PlayerController>();
        exitPortal = GameObject.FindObjectOfType<ExitPortal>();
        player.onOrbit.AddListener(AddStar);
        fuseDelegate = FuseStars;
        starDecrementDelegate = DecrementStarWaitCount;
    }

    // Update is called once per frame
    void Update()
    {
        if(!startedFuse && orbittedStars.Count >= requiredStarsForFusion){
            player.onLaunch.AddListener(fuseDelegate);
            startedFuse = true;
        }
       
        bool allStarsFused = CheckRequiredStars();
        if(allStarsFused){
            exitPortal.OpenPortal();
        }
    }

    /// <summary>
    /// Adds star to the required stars list. This star must be fused in order for the portal to be opened.
    /// </summary>
    /// <param name="star"></param>
    public void Register(Star star){
        requiredStars.Add(star);
    }

    bool CheckRequiredStars(){
        foreach(Star star in requiredStars){
            if(!star.isFusing()){
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Adds the star that the player is orbitting to the 'star recipe'
    /// </summary>
    void AddStar(){
        Star star = player.GetOrbittedObject().GetComponent<Star>();
        if(star){
            orbittedStars.Add(star);

            star.SetReadyForFusion();
        }
    }


    void FuseStars() {
        starsToFuse = new List<Star>(orbittedStars);
        orbittedStars.Clear();
        player.onLaunch.RemoveListener(fuseDelegate);

        Vector3 center = CalculateCenter(starsToFuse);
        StartCoroutine(WaitForStars(starsToFuse));
 
        foreach (Star star in starsToFuse) {
            star.StartFusing(center);
        }
    }

    IEnumerator WaitForStars(List<Star> stars) {
        numOfStarsWaitingFor = stars.Count;

        foreach (Star star in stars) {
            star.onReachDestination.AddListener(starDecrementDelegate);
        }

        while(numOfStarsWaitingFor > 0) {
            yield return new WaitForEndOfFrame();
        }
        
        foreach(Star star in stars) {
            star.onReachDestination.RemoveListener(starDecrementDelegate);
        }

        ResetStars();
    }

    void DecrementStarWaitCount() {
        numOfStarsWaitingFor--;
    }

    void ResetStars() {
        foreach(Star star in starsToFuse) {
            star.gameObject.SetActive(false);
        }
        startedFuse = false;
        starsToFuse.Clear();
    }

    Vector3 CalculateCenter(List<Star> stars) {
        float X = 0f;
        float Y = 0f;
        foreach(Star star in stars) {
            X += star.transform.position.x;
            Y += star.transform.position.y;
        }

        return new Vector3(X / stars.Count, Y / stars.Count, 0);
    }
}
