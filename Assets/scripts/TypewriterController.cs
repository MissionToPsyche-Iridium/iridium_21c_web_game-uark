using UnityEngine;
using TMPro;
using ChristinaCreatesGames.Typography.Typewriter;

public class TypewriterController : MonoBehaviour
{
    public TypewriterEffect typewriterEffect;
    public TMP_Text textMeshPro;
    public GameObject skipButton;

    [TextArea(3,10)] //allows for multi-line input
    public string message; // Default message

    private void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            skipButton.SetActive(true);
        }
    }

    private void OnEnable()
    {
        Debug.Log("TypewriterEffect Activated: " + gameObject.name);
        textMeshPro.text = message;  // Assign the new text
        textMeshPro.ForceMeshUpdate();
        typewriterEffect.RestartEffect(); // Start typewriter effect
    }
}
