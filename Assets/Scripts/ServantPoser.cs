using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// On every frame, 'poses' the servant according to the currently active posing function.
/// Switches the pose function every time the player launches.
/// </summary>
public class ServantPoser : MonoBehaviour
{
    [SerializeField] CursorOrbitter orbitter;
    public GameObject playerMesh;

    PlayerController player;
    GameObject cursor;
    UnityAction poseFunction;

    bool poseFlag = false;

    private void Start() {
        player = GameObject.FindObjectOfType<PlayerController>();
        SetNextPoseFunction();
        player.onLaunch.AddListener(SetNextPoseFunction);
        cursor = orbitter.GetCursor();
    }

    // Update is called once per frame
    void Update()
    {
        poseFunction.Invoke();
    }

    void SetNextPoseFunction(){
        
        if(!poseFlag){
            poseFunction = PointTowardsCursor;
        }else{
            playerMesh.transform.right = cursor.transform.position - playerMesh.transform.position;
            poseFunction = RotateAroundYAxis;
        }
        poseFlag = !poseFlag;
    }

    void PointTowardsCursor(){
        Vector3 direction = cursor.transform.position - playerMesh.transform.position;        
        playerMesh.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
    }

    void RotateAroundYAxis(){
        playerMesh.transform.Rotate(transform.up, 300 * Time.deltaTime, Space.Self);
    }
}
