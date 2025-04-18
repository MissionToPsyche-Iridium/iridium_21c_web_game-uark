using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dial : MonoBehaviour
{
    public int dialNum = -1;
    public float min, max;
    [SerializeField] GameObject lowerScreen;
    private Renderer screenRenderer;

    private float prevX;
    private float turnSpeed = 2.5f;
    private float ampFactor = 0.005f;
    private float lengthFactor = 0.01f;
    private bool dragLock = false;


    // Start is called before the first frame update
    void Start()
    {
        screenRenderer = lowerScreen.GetComponent<Renderer>();
    }

    void OnMouseDown()
    {
        // Set starting value for previous x position on first click
        prevX = Input.mousePosition.x;
        // Remove lock on dragging
        dragLock = false;
    }

    void OnMouseDrag()
    {
        // Don't keep dragging if lock active
        if(dragLock)
            return;
        
        // Calculate change in mouse x position
        float deltaX = Input.mousePosition.x - prevX;
        // Rotate dial according to change in x
        this.gameObject.transform.Rotate(Vector3.up, deltaX * turnSpeed * Time.deltaTime, Space.World);

        // Handle left (amplitude) dial
        if(dialNum == 0)
        {
            // Calculate new amplitude based on deltaX and current amplitude
            float amplitude = screenRenderer.material.GetFloat("_Amplitude") + deltaX * ampFactor;
            // Limit amplitude to given bounds
            if(amplitude < min)
                amplitude = min;
            else if(amplitude > max)
                amplitude = max;
            // Set amplitude in lower screen's shader
            screenRenderer.material.SetFloat("_Amplitude", amplitude);
            // Lock drag if mouse goes to right side of screen
            if(Input.mousePosition.x > Screen.width/2)
                dragLock = true;
        }
        // Handle right (wavelength) dial
        else if(dialNum == 1)
        {
            // Calculate new wavelength based on deltaX and current wavelength
            float wavelength = screenRenderer.material.GetFloat("_Wavelength") + deltaX * lengthFactor;
            // Limit wavelength to given bounds
            if(wavelength < min)
                wavelength = min;
            else if(wavelength > max)
                wavelength = max;
            // Set wavelength in lower screen's shader
            screenRenderer.material.SetFloat("_Wavelength", wavelength);
            // Lock drag if mouse goes to left side of screen
            if(Input.mousePosition.x < Screen.width/2)
                dragLock = true;
        }
        // Set current x position as previous x position for next frame
        prevX = Input.mousePosition.x;
    }

}
