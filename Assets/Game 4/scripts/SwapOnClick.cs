using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class SwapOnClick : MonoBehaviour
{
    public List<string> validTags = new List<string> { "GreenGem", "BlueGem", "RedGem", "YellowGem", "OrangeGem", "PinkGem", "RockGem" };  // List of valid tags

    public static GameObject selectedObject = null;  // The second selected object
    public static GameObject firstSelectedObject = null;  // The first selected object
    public static bool isColliding = false; // To track if two objects are colliding

    void Update()
    {
        // Check if the left mouse button was clicked
        if (Input.GetMouseButtonDown(0))
        {
            // Raycast from the mouse position to detect the clicked object
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.transform.gameObject;

                // Check if the clicked object has one of the valid tags
                if (validTags.Contains(clickedObject.tag))
                {
                    // If no object has been selected, this is the first object selected
                    if (firstSelectedObject == null)
                    {
                        firstSelectedObject = clickedObject;
                        //Debug.Log(firstSelectedObject.name + " selected");
                    }
                    // If the first object has been selected, now select the second object
                    else if (selectedObject == null && clickedObject != firstSelectedObject)
                    {
                        selectedObject = clickedObject;
                        //Debug.Log(selectedObject.name + " selected");
                        //will reset clickedObject 1 and 2 after 0.5 seconds
                        Invoke("DelayedMethod", 0.25f);
                    }
                }
            }
        }

        // If both objects are selected and colliding, swap them
        if (firstSelectedObject != null && selectedObject != null && isColliding)
        {
            SwapPositions(firstSelectedObject, selectedObject);
            ResetSelection(); // Reset after swapping
        }
    }

    void SwapPositions(GameObject obj1, GameObject obj2)
    {
        // Store the positions temporarily
        Vector3 tempPosition = obj1.transform.position;

        // Swap the positions
        obj1.transform.position = obj2.transform.position;
        obj2.transform.position = tempPosition;
    }

    // Reset selection after swap
    void ResetSelection()
    {
        firstSelectedObject = null;
        selectedObject = null;
        isColliding = false; // Reset the collision state
    }

    void DelayedMethod()
    {
        ResetSelection();
    }
}
