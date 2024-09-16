using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    public bool focusOnObject = false; // Variable to control the camera focus
    public GameObject targetObject; // The game object to focus on

    private Vector3 originalPos; // Original position of the camera
    private float speed = 3.0f; // Speed of camera movement

    void Start()
    {
        
    }

    void Update()
    {
        if (focusOnObject==true)
        {   Debug.Log("Camera Focus set to true");
            originalPos = transform.position;// Store the original position of the camera
            // Move the camera smoothly towards the target object
            Vector3 targetPos = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
            focusOnObject=false;
            Debug.Log("Camera Focus set to false");
            // Move the camera smoothly back to its original position
            transform.position = Vector3.Lerp(transform.position, originalPos, speed * Time.deltaTime);

            
        }
        
    }
}
