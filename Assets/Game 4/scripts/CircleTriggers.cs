using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleTriggers : MonoBehaviour
{
    // A HashSet to keep track of unique colliders currently within the trigger
    private HashSet<GameObject> collidingObjects = new HashSet<GameObject>();
    public int count;
    public string parentTag;

    void Start()
    {
        if (transform.parent != null)
        {
            parentTag = transform.parent.tag;
        }
    }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log(gameObject.tag);
        // Ensure transform.parent is not null before accessing it //gameObject.tag
        if (other.gameObject.CompareTag(parentTag))
        {
            // Add the colliding object to the HashSet (only unique objects are stored)
            collidingObjects.Add(other.gameObject);
        }

        // Ensure firstSelectedObject and selectedObject are not null before accessing them
        if (SwapOnClick.firstSelectedObject != null && SwapOnClick.selectedObject != null)
        {
            // Ensure both the parent and the other object are the correct ones
            if (transform.parent != null && transform.parent.gameObject == SwapOnClick.firstSelectedObject && other.gameObject == SwapOnClick.selectedObject)
            {
                SwapOnClick.isColliding = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Remove the object from the HashSet when it exits the trigger
        collidingObjects.Remove(other.gameObject);
    }

    void Update()
    {
        count = collidingObjects.Count;

        if (collidingObjects.Count >= 2 && IsVelocityZero())
        {
            List<GameObject> objectsToDestroy = new List<GameObject>(); // Store objects to destroy

            foreach (GameObject obj in collidingObjects)
            {
                if (obj != null) 
                {
                    objectsToDestroy.Add(obj); // Add to list for deletion later

                    // Check if the parent is NOT tagged as "Rows"
                    if (obj.transform.parent != null && !obj.transform.parent.CompareTag("Rows"))
                    {
                        objectsToDestroy.Add(obj.transform.parent.gameObject);
                    }
                }
            }

            // Destroy the objects in the list
            foreach (GameObject toDestroy in objectsToDestroy)
            {
                Destroy(toDestroy);
            }

            if (GameManager4.Instance == null)
            {
                Debug.LogError("GameManager4 instance not found!");
                return;
            }

            switch (parentTag)
            {
                case "BlueGem": GameManager4.Instance.IncrementNickel(); break;
                case "GreenGem": GameManager4.Instance.IncrementIron(); break;
                case "OrangeGem": GameManager4.Instance.IncrementSilicon(); break;
                case "PinkGem": GameManager4.Instance.IncrementPotassium(); break;
                case "RedGem": GameManager4.Instance.IncrementSulfur(); break;
                case "RockGem": GameManager4.Instance.IncrementAluminum(); break;
                case "YellowGem": GameManager4.Instance.IncrementCalcium(); break;
                default: Debug.LogWarning("Switch variable is out of range for gem colors"); break;
            }

            // Destroy the current object and its direct parent (if it's not tagged "Rows")
            if (transform.parent != null && !transform.parent.CompareTag("Rows"))
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(gameObject);

            // Clear HashSet after deletion
            collidingObjects.Clear();
        }
    }


    private bool IsVelocityZero()
    {
        // Check if the parent exists
        if (transform.parent != null)
        {
            // Try to get the Rigidbody attached to the parent GameObject
            Rigidbody rb = transform.parent.GetComponent<Rigidbody>();

            // Check if the Rigidbody exists and its velocity is close to zero
            if (rb != null)
            {
                return rb.velocity.magnitude < 0.01f; // Threshold for "zero" velocity
            }
        }

        // Return true if there is no parent or no Rigidbody found (or handle it as needed)
        return true; // Assuming velocity is zero if no Rigidbody is found
    }

}
