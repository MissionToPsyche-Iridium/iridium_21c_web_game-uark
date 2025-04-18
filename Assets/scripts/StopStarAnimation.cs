using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopStarAnimation : MonoBehaviour
{
    private TalkingCharacterUI talkingCharacter;

    void Start()
    {
        // Find the TalkingCharacter script on the same GameObject or in the scene
        talkingCharacter = FindObjectOfType<TalkingCharacterUI>();

        talkingCharacter.StopTalking();
    }

}

