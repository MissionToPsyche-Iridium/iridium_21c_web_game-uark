using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public void IncreaseCount()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(); // Increase level beat by 1
        }
    }
    public void Reset()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetGame(); // Increase level beat by 1
        }
    }
}
