using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance
    public AudioClip clickSFX;
    public AudioSource audioSource;
    public AudioSource soundtrack;
    // Example game variables
    public int gameFinished;
    public string playerName;
    private bool hasCheckedReveal = false; // Prevents multiple calls

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object across scenes
            SceneManager.sceneLoaded += OnSceneLoaded; // Attach event listener
        }
        else
        {
            Destroy(gameObject); // Prevent duplicate GameManagers
        }

        if(soundtrack != null)
        {
            soundtrack.loop = true;
            soundtrack.Play();
        }
        else
        {
            Debug.LogWarning("Soundtrack source not assigned");
        }
    }

    private void Update()
    {
        // Play SFX for any mouse click (left, right, or middle)
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            audioSource.PlayOneShot(clickSFX);
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "home" && !hasCheckedReveal)
        {
            Debug.Log("Home scene loaded, running CheckRevealObject() once.");
            CheckRevealObject();
            hasCheckedReveal = true; // Prevent multiple calls
        }
        else
        {
            hasCheckedReveal = false; // Reset when leaving "home" scene
        }

        if (scene.name == "Game1" || scene.name == "Game2" || scene.name == "Game3" || scene.name == "Game4")
        {
            Debug.Log("Level loaded");
            StartGameText();
        }

    }

    private void StartGameText()
    {
        GameObject objToReveal1 = GameObject.FindWithTag("RevealUI1");
        objToReveal1.transform.GetChild(1).gameObject.SetActive(true);
    }

    //reveals UI in the home page based on what game is completed
    private void CheckRevealObject()
    {
        GameObject objToReveal1 = GameObject.FindWithTag("RevealUI1");
        GameObject objToReveal2 = GameObject.FindWithTag("RevealUI2"); 
        GameObject objToReveal3 = GameObject.FindWithTag("RevealUI3"); 
        GameObject objToReveal4 = GameObject.FindWithTag("RevealUI4");
        GameObject objToReveal5 = GameObject.FindWithTag("RevealUI5");

        objToReveal1.transform.GetChild(0).gameObject.SetActive(false);
        objToReveal2.transform.GetChild(0).gameObject.SetActive(false);
        objToReveal3.transform.GetChild(0).gameObject.SetActive(false);
        objToReveal4.transform.GetChild(0).gameObject.SetActive(false);
        objToReveal5.transform.GetChild(0).gameObject.SetActive(false);

        switch (gameFinished)
        {
            case 0:
                Debug.Log("lvl 0, home base ui displaying");
                objToReveal1.transform.GetChild(0).gameObject.SetActive(true);
                break;
            
            case 1:
                Debug.Log("lvl 1 has been beat, displaying ui to level 2");
                objToReveal2.transform.GetChild(0).gameObject.SetActive(true);
                break;

            case 2:
                Debug.Log("lvl 2 has been beat, displaying ui to level 3");
                objToReveal3.transform.GetChild(0).gameObject.SetActive(true);
                break;

            case 3:
                Debug.Log("lvl 3 has been beat, displaying ui to level 4");
                objToReveal4.transform.GetChild(0).gameObject.SetActive(true);
                break;

            case 4:
                Debug.Log("lvl 4 has been beat, displaying end game ui");
                objToReveal5.transform.GetChild(0).gameObject.SetActive(true);
                break;

            default:
                Debug.Log("default switch case");
                break;
        }
    }
    public void AddScore()
    {
        gameFinished++;
        Debug.Log("level finished: " + gameFinished);
    }

    public void ResetGame()
    {
        // Reset all variables when called
        gameFinished = 0;
        playerName = "Player";

        Debug.Log("Game Reset!");
    }

}
