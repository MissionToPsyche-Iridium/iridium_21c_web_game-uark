using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropCircles : MonoBehaviour
{
    public GameObject[] spherePrefabs;  // Array to hold 4 different sphere prefabs
    public float dropInterval = 1f;     // Time interval between drops
    public int maxDrops = 1;            // Max number of times the spheres drop

    public int dropCount = 0;          // Count the number of drops

    void Start()
    {
        InvokeRepeating("DropSphere", 0f, dropInterval);
    }

    void DropSphere()
    {
        if (dropCount < maxDrops)
        {
            // Randomly choose one of the 4 spheres
            int randomIndex = Random.Range(0, spherePrefabs.Length); // Random index between 0 and 3
            GameObject chosenSphere = spherePrefabs[randomIndex];   // Get the randomly chosen sphere prefab

            // Instantiate the chosen sphere at a random position above the object
            Vector3 dropPosition = new Vector3(transform.position.x, transform.position.y + 20f, transform.position.z);
            Instantiate(chosenSphere, dropPosition, Quaternion.identity);

            // Increment the drop count
            dropCount++;
        }
        else
        {
            // Stop dropping spheres after reaching the max limit
            CancelInvoke("DropSphere");
        }
    }
}
