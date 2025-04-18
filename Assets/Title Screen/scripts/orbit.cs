using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class orbit : MonoBehaviour
{
    [SerializeField] GameObject asteroid;
    [SerializeField] GameObject playerCamera;
    public float orbitSpeed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.RotateAround(asteroid.transform.position, Vector3.up, orbitSpeed * Time.deltaTime);
        //this.gameObject.transform.Rotate(Vector3.up, orbitSpeed * Time.deltaTime, Space.World);
        //playerCamera.gameObject.transform.Rotate(Vector3.up, -orbitSpeed * Time.deltaTime, Space.World);
    }
}
