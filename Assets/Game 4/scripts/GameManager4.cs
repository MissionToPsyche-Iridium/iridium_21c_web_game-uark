using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // Import SceneManager

public class GameManager4 : MonoBehaviour
{
    // Static instance for global access
    public static GameManager4 Instance;

    // Assign these in the Inspector
    public TMP_Text text1, text2, text3, text4, text5, text6, text7;

    // Internal counters for each TMP_Text
    public int count1 = 0, count2 = 0, count3 = 0, count4 = 0, count5 = 0, count6 = 0, count7 = 0;

    public GameObject NickelUI, IronUI, SiliconUI, PotassiumUI, SulfurUI, AluminumUI, CalciumUI, EndUI, RestartUI;

    private bool allDetected = false; // Flag to prevent multiple triggers
    public GameObject instructionsPopUp;
    public KeyCode toggleKey = KeyCode.I;
    public AudioClip swapSFX;
    public AudioSource audioSource;


    // Set up the singleton instance
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Debug.Log("Game 4 has now started");
        ResetUI();
        RestartUI.SetActive(true);
        EndUI.SetActive(false);
    }

    private void ResetUI()
    {
        NickelUI.SetActive(false);
        IronUI.SetActive(false);
        SiliconUI.SetActive(false);
        PotassiumUI.SetActive(false);
        SulfurUI.SetActive(false);
        AluminumUI.SetActive(false);
        CalciumUI.SetActive(false);
    }

    // Function to reset the scene
    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        //logic for turning instructions on and off with 'I' key
        if(Input.GetKeyDown(toggleKey))
        {
            instructionsPopUp.SetActive(!instructionsPopUp.activeSelf);
        }

        // Check if all counts have reached 12
        if (!allDetected && count1 == 12 && count2 == 12 && count3 == 12 && count4 == 12 &&
            count5 == 12 && count6 == 12 && count7 == 12)
        {
            allDetected = true; // Prevents multiple triggers
            RestartUI.SetActive(false);
            StartCoroutine(DelayedOnAllElementsDetected()); //waits 5 seconds
        }
    }

    private IEnumerator DelayedOnAllElementsDetected()
    {
        yield return new WaitForSeconds(5f);
        OnAllElementsDetected();
    }

    private void OnAllElementsDetected()
    {
        Debug.Log("All elements have been detected!");
        EndUI.SetActive(true);
    }

    public void IncrementNickel()
    {
        UpdateElement(ref count1, text1, "Nickel", NickelUI);
    }

    public void IncrementIron()
    {
        UpdateElement(ref count2, text2, "Iron", IronUI);
    }

    public void IncrementSilicon()
    {
        UpdateElement(ref count3, text3, "Silicon", SiliconUI);
    }

    public void IncrementPotassium()
    {
        UpdateElement(ref count4, text4, "Potassium", PotassiumUI);
    }

    public void IncrementSulfur()
    {
        UpdateElement(ref count5, text5, "Sulfur", SulfurUI);
    }

    public void IncrementAluminum()
    {
        UpdateElement(ref count6, text6, "Aluminum", AluminumUI);
    }

    public void IncrementCalcium()
    {
        UpdateElement(ref count7, text7, "Calcium", CalciumUI);
    }

    private void UpdateElement(ref int count, TMP_Text text, string name, GameObject ui)
    {
        //correct swap sfx
        audioSource.PlayOneShot(swapSFX);
        
        count += 3;
        if (text != null)
            text.text = $"{name} Detected: {count}/12";

        if (count == 12)
        {
            ui.SetActive(true);
        }
    }

}
