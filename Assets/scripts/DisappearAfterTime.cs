using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using TMPro;

public class DisappearAfterTime : MonoBehaviour
{
    // Set the time delay in seconds
    public float timeToDisappear = 5f;
    public GameObject objectToActivate;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    IEnumerator Disappear()
    {
        // Wait for the specified amount of time
        yield return new WaitForSeconds(timeToDisappear);

        // Activate the referenced GameObject
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Object to activate is not assigned in the inspector.");
        }

        // Destroy the game object this script is attached to
        Destroy(gameObject);
    }

    public void InstantSkip()
    {
        // Activate the referenced GameObject
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
        else
        {
            Debug.LogWarning("Object to activate is not assigned in the inspector.");
        }

        // Destroy the game object this script is attached to
        Destroy(gameObject);
    }
}