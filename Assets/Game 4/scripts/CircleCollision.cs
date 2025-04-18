using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleCollision : MonoBehaviour
{
    // This will be triggered when any object collides with the first selected object
    void OnCollisionStay(Collision collision)
    {
        //Debug.Log("collision");
        // // Check if the collision is between the selected objects
        // if (this.gameObject == SwapOnClick.firstSelectedObject && collision.gameObject == SwapOnClick.selectedObject)
        // {
        //     SwapOnClick.isColliding = true; // Objects are colliding, allow the swap
        //     Debug.Log("Objects are colliding, swap is allowed.");
        // }
        // if (this.gameObject == SwapOnClick.firstSelectedObject)
        // {
        //     Debug.Log("first object is called " + gameObject.name);
        // }
    }


    void Update()
    {
        //Debug.Log(SwapOnClick.firstSelectedObject);
        //Debug.Log(SwapOnClick.selectedObject);
    }
}
