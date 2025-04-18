using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager1 : MonoBehaviour
{
    [SerializeField] GameObject asteroid;
    [SerializeField] GameObject selector;
    private Vector2 selectorScreenPos = new Vector2();
    private Renderer selectorRenderer;
    private const int numFeatures = 4;
    [SerializeField] GameObject[] features = new GameObject[numFeatures];
    private static bool[] featureFound = new bool[numFeatures];
    private static int numFound = 0;
    private static int focusFeature = -1;
    private Vector2[] featureScreenPos = new Vector2[numFeatures];
    public float[] selectDistance = new float[numFeatures];
    public float nearbyOffset = 20.0f;
    public Vector2[] focusBounds = new Vector2[numFeatures];
    [SerializeField] Camera mainCamera;
    public GameObject instructionsPopUp;
    public GameObject endLevelUI;
    public KeyCode toggleKey = KeyCode.I;
    private bool triggered = false;

    // Start is called before the first frame update
    void Start()
    {
        selectorRenderer = selector.GetComponent<Renderer>();
        for(int i = 0; i < numFeatures; i++)
            featureFound[i] = false;
        Debug.Log("Game 1 has now started");
    }

    // Update is called once per frame
    void Update()
    {
        //logic for turning instructions on and off with 'I' key
        if(Input.GetKeyDown(toggleKey))
        {
            instructionsPopUp.SetActive(!instructionsPopUp.activeSelf);
        }

        selectorScreenPos = mainCamera.WorldToScreenPoint(selector.transform.position);
        selectorRenderer.material.SetColor("_Color", Color.white);
        int colorIndex = -1;
        for(int i = 0; i < numFeatures; i++)
        {
            int temp = FeatureInFocus(i);
            if(temp > colorIndex)
            {
                colorIndex = temp;
                focusFeature = i;
            }
        }
        if(colorIndex == 0)
            selectorRenderer.material.SetColor("_Color", Color.cyan);
        else if(colorIndex == 1)
        {
            if(focusFeature >= 0 && featureFound[focusFeature] == true)
                selectorRenderer.material.SetColor("_Color", Color.green);
            else
                selectorRenderer.material.SetColor("_Color", Color.yellow);
        }

        //win condition yay
        if(numFound >= numFeatures && !triggered)
        {
            triggered = true;
            StartCoroutine(WaitAndExecute());
        }
            
    }

    private IEnumerator WaitAndExecute()
    {
        yield return new WaitForSeconds(8f); //waits 8 seconds
        endLevelUI.SetActive(true);
    }

    int FeatureInFocus(int featureNum)
    {
        // Get feature's position in screen space
        featureScreenPos[featureNum] = mainCamera.WorldToScreenPoint(features[featureNum].transform.position);
        // Check if at correct zoom to focus in on feature
        if(asteroid.transform.localScale.x > focusBounds[featureNum].y || asteroid.transform.localScale.x < focusBounds[featureNum].x)
        {
            // If wrong zoom but correct position, return 0 to set selector color to blue
            if(Vector2.Distance(selectorScreenPos, featureScreenPos[featureNum]) < selectDistance[featureNum] + nearbyOffset)
                return 0;
            // If wrong zoom and position, return -1 to keep selector color white
            return -1;
        }
        // Zoom is correct; if correct position, return 1 to set selector to yellow
        if(Vector2.Distance(selectorScreenPos, featureScreenPos[featureNum]) < selectDistance[featureNum])
            return 1;
        // If all other cases fail, return -1 to keep selector color white
        return -1;
    }

    public static int SelectCurrentFeature()
    {
        if(focusFeature >= 0 && focusFeature < numFeatures)
        {
            featureFound[focusFeature] = true;
            numFound++;
            return focusFeature;
        }
        return -1;
    }
}
