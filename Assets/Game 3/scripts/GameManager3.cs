using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager3 : MonoBehaviour
{

    [SerializeField] GameObject lowerScreen;
    [SerializeField] GameObject upperScreen;
    private Renderer lowerRenderer;
    private Renderer upperRenderer;

    private int[] active = new int[7];
    private float[] waveAmps = new float[7];
    private float[] waveLens = new float[7];
    private int numWaves = 6;
    private float noiseDegree;
    public GameObject instructionsPopUp;
    public KeyCode toggleKey = KeyCode.I;
    public bool win = false;
    public GameObject EndUI;
    public AudioClip swapSFX;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game 3 has now started");
        EndUI.SetActive(false);
        lowerRenderer = lowerScreen.GetComponent<Renderer>();
        upperRenderer = upperScreen.GetComponent<Renderer>();

        noiseDegree = (upperRenderer.material.GetFloat("_Noise_Scale_0") - 0.02f)/6;
    }

    // Update is called once per frame
    void Update()
    {
        //logic for turning instructions on and off with 'I' key
        if(Input.GetKeyDown(toggleKey))
        {
            instructionsPopUp.SetActive(!instructionsPopUp.activeSelf);
        }

        if(numWaves == 0 && !win)
        {
            win = true;
            Debug.Log("You win!");
            lowerRenderer.material.SetFloat("_Amplitude", 0.0f);
            EndUI.SetActive(true);
        }

        float amplitude = lowerRenderer.material.GetFloat("_Amplitude");
        float wavelength = lowerRenderer.material.GetFloat("_Wavelength");

        for(int i = 1; i <= 6; i++)
        {
            active[i] = upperRenderer.material.GetInt("_Active_"+i);
            waveAmps[i] = upperRenderer.material.GetFloat("_Amplitude_"+i);
            waveLens[i] = upperRenderer.material.GetFloat("_Wavelength_"+i);
        }

        for(int i = 1; i <= 6; i++)
        {
            if(active[i] == 1 && Mathf.Abs(amplitude - waveAmps[i]) < 0.05 && Mathf.Abs(wavelength - waveLens[i]) < 0.1)
            {
                upperRenderer.material.SetInt("_Active_"+i, 0);
                upperRenderer.material.SetFloat("_Noise_Scale_0", upperRenderer.material.GetFloat("_Noise_Scale_0") - noiseDegree);
                numWaves--;
                //correct swap sfx
                audioSource.PlayOneShot(swapSFX);
            }
        }
    }
}
