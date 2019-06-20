using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Responsible for loading the different screens, as well as resetting the screens
/// if a restart is called.
/// </summary>
public class ScreenLoader : MonoBehaviour
{
    // The time to wait AFTER LoadNextScreen is called, BEFORE the next screen is loaded
    [SerializeField] float timeToExit = 2f;
    // The object containing the Start Menu UI, which will be disabled when starting the game
    [SerializeField] GameObject startMenu;
    // A screen should be a gameobject with the player, all the stars, and the exit, as well as any other props needed.
    public GameObject[] Levels;

    Vector3 screenSpawnLocation = Vector3.zero;
    GameObject currentScreen = null;
    bool gameIsLoaded = false;
    PlayerController currentPlayer;
    int currentScreenIndex = 0;

    // true when a level is about to load, false otherwise
    bool isScreenLoading = false;

    void Update()
    {
        if(!gameIsLoaded && Input.GetKeyDown(KeyCode.Space)){
            startMenu.SetActive(false);
            LoadScreen(currentScreenIndex);
        } else if(gameIsLoaded && !isScreenLoading && Input.GetKeyDown(KeyCode.R)){
            ResetScreen();
        }
    }

    /// <summary>
    /// Loads the screen found at the given index in Levels.
    /// </summary>
    /// <param name="screenIndex"></param>
    void LoadScreen(int screenIndex){
        if(screenIndex >= Levels.Length){
            Debug.Log("End reached.");
            return;
        }
        
        GameObject screen = Levels[screenIndex];
        currentScreen = GameObject.Instantiate<GameObject>(screen, screenSpawnLocation, screen.transform.rotation);
        gameIsLoaded = true;
        currentPlayer = GameObject.FindObjectOfType<PlayerController>();
        currentPlayer.onExit.AddListener(StartLoadingNextScreen);
    }

    void StartLoadingNextScreen(){
        StartCoroutine(LoadNextScreen());
    }

    IEnumerator LoadNextScreen(){
        isScreenLoading = true;
        yield return new WaitForSeconds(timeToExit);
        DestroyCurrentScreen();
        currentScreenIndex++;
        LoadScreen(currentScreenIndex);
        isScreenLoading = false;
    }

    void DestroyCurrentScreen(){
        currentPlayer = null;
        Destroy(currentScreen);
    }

    void ResetScreen(){
        DestroyCurrentScreen();
        LoadScreen(currentScreenIndex);
    }
}
