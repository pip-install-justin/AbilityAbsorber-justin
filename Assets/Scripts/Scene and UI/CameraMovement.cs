using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMovement : MonoBehaviour {

    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;
    public Vector2 overviewEndPoint;
    public float overviewSpeed = 1f;
    private bool gameStarted = false;
    private Vector3 originalPosition;
    public PlayerMovement playerMov;
    public static event Action OnOverviewComplete;

    public bool focusOnObject = false; // Variable to control the camera focus
    public GameObject targetObject; // The game object to focus on
    private float focusSpeed = 3.0f; // Speed of camera focus movement

    public float zoomSpeed = 1f; // Speed of camera zoom
    private float initialSize; // Size to reset to
    public float zoomedSize = 5f; // Size when zoomed in


    public static bool isRestarting= false;



    void Start () {
        originalPosition = transform.position;
        initialSize = Camera.main.orthographicSize; // Save initial size

        if (!isRestarting) {
        StartCoroutine(StartGameOverview());
    } else {
        // If the game is restarting, we reset the flag and enable the player immediately.
        isRestarting = false;
        playerMov.enabled = true;
        gameStarted = true;
    }
    }

    IEnumerator StartGameOverview() {
    Vector3 endPosition = new Vector3(overviewEndPoint.x, overviewEndPoint.y, transform.position.z);
    playerMov.enabled = false;

    float t = 0;
    while (t < 1) {
        t += Time.deltaTime * overviewSpeed;
        transform.position = Vector3.Lerp(originalPosition, endPosition, t);
        yield return null;
    }

    // Wait for two seconds after reaching the endpoint
    yield return new WaitForSeconds(1);

    t = 0;
    while (t < 1) {
        t += Time.deltaTime * overviewSpeed;
        transform.position = Vector3.Lerp(endPosition, originalPosition, t);
        yield return null;
    }

    gameStarted = true;
    OnOverviewComplete?.Invoke();
    playerMov.enabled = true;
}

    
     void LateUpdate () {
        if(focusOnObject)
        {
            Vector3 targetPos = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, transform.position.z);
            targetPos = ClampPosition(targetPos);
            transform.position = Vector3.Lerp(transform.position, targetPos, focusSpeed * Time.deltaTime);
            
            // Zoom in
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, zoomedSize, Time.deltaTime * zoomSpeed);

            if(Vector3.Distance(transform.position, targetPos) < 0.01f)
            {
                focusOnObject = false; // Once we reached target, stop focusing
            }
        }
        else if(gameStarted && transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x,
                                                 target.position.y,
                                                 transform.position.z);
            targetPosition = ClampPosition(targetPosition);
            
            transform.position = Vector3.Lerp(transform.position,
                                             targetPosition, smoothing);

            // Zoom out
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, initialSize, Time.deltaTime * zoomSpeed);
        }
    }

    // Function to clamp the camera position within defined bounds
    Vector3 ClampPosition(Vector3 targetPos) 
    {
        targetPos.x = Mathf.Clamp(targetPos.x, minPosition.x, maxPosition.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minPosition.y, maxPosition.y);

        return targetPos;
    }

    private Vector3 RoundPosition(Vector3 position)
    {
        float xOffset = position.x % .0625f;
        if(xOffset != 0)
        {
            position.x -= xOffset;
        }
        float yOffset = position.y % .0625f;
        if(yOffset != 0)
        {
            position.y -= yOffset;
        }
        return position;
    }
}

/*
//code without focus :

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

    public Transform target;
    public float smoothing;
    public Vector2 maxPosition;
    public Vector2 minPosition;
    public Vector2 overviewEndPoint;
    public float overviewSpeed = 1f;
    private bool gameStarted = false;
    private Vector3 originalPosition;
    public PlayerMovement playerMov;
    public static event Action OnOverviewComplete;

    public bool focusOnObject = false; // Variable to control the camera focus
    public GameObject targetObject; // The game object to focus on
    private float focusSpeed = 3.0f; // Speed of camera focus movement

    void Start () {
        originalPosition = transform.position;
        StartCoroutine(StartGameOverview());
    }

    IEnumerator StartGameOverview() {
        Vector3 endPosition = new Vector3(overviewEndPoint.x, overviewEndPoint.y, transform.position.z);
        playerMov.enabled = false;

        float t = 0;
        while (t < 1) {
            t += Time.deltaTime * overviewSpeed;
            transform.position = Vector3.Lerp(originalPosition, endPosition, t);
            yield return null;
        }

        t = 0;
        while (t < 1) {
            t += Time.deltaTime * overviewSpeed;
            transform.position = Vector3.Lerp(endPosition, originalPosition, t);
            yield return null;
        }

        gameStarted = true;
        OnOverviewComplete?.Invoke();
        playerMov.enabled = true;
    }
    
    void LateUpdate () {
        if(focusOnObject)
        {
            Vector3 targetPos = new Vector3(targetObject.transform.position.x, targetObject.transform.position.y, transform.position.z);
            targetPos = ClampPosition(targetPos);
            transform.position = Vector3.Lerp(transform.position, targetPos, focusSpeed * Time.deltaTime);
            if(Vector3.Distance(transform.position, targetPos) < 0.01f)
            {
                focusOnObject = false; // Once we reached target, stop focusing
            }
        }
        else if(gameStarted && transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x,
                                                 target.position.y,
                                                 transform.position.z);
            targetPosition = ClampPosition(targetPosition);
            
            transform.position = Vector3.Lerp(transform.position,
                                             targetPosition, smoothing);
        }
    }

    // Function to clamp the camera position within defined bounds
    Vector3 ClampPosition(Vector3 targetPos) 
    {
        targetPos.x = Mathf.Clamp(targetPos.x, minPosition.x, maxPosition.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minPosition.y, maxPosition.y);

        return targetPos;
    }

    private Vector3 RoundPosition(Vector3 position)
    {
        float xOffset = position.x % .0625f;
        if(xOffset != 0)
        {
            position.x -= xOffset;
        }
        float yOffset = position.y % .0625f;
        if(yOffset != 0)
        {
            position.y -= yOffset;
        }
        return position;
    }
}

*/