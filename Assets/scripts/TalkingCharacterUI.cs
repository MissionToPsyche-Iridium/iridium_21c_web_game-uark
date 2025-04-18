using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TalkingCharacterUI : MonoBehaviour
{
    public Sprite mouthClosed;
    public Sprite mouthOpen;
    private Image characterImage;
    public bool isTalking = false;

    void Start()
    {
        characterImage = GetComponent<Image>();
        StartTalking(); // Start animation loop
    }

    public void StartTalking()
    {
        isTalking = true;
        InvokeRepeating(nameof(ToggleMouth), 0.15f, 0.15f); // Flips image every 0.2s
    }

    public void StopTalking()
    {
        isTalking = false;
        CancelInvoke(nameof(ToggleMouth));
        characterImage.sprite = mouthClosed; // Reset to closed mouth
    }

    private void ToggleMouth()
    {
        characterImage.sprite = characterImage.sprite == mouthClosed ? mouthOpen : mouthClosed;
    }
}