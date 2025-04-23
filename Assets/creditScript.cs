using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class creditScript : MonoBehaviour
{
    public GameObject credits;

    // Start is called before the first frame update
    void Start()
    {
        credits.SetActive(false);
    }


    public void ShowCredits()
    {
        credits.SetActive(!credits.activeSelf);
    }
}
