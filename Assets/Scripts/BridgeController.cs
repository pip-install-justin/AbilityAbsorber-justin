using UnityEngine;

/*
public class BridgeController : MonoBehaviour
{
    public Transform targetPosition;   // Drag the target position (endpoint) here in the inspector.
    public float extendSpeed = 2.0f;   // Speed at which the bridge extends.
    private bool shouldExtend = false; // Boolean flag to check if the bridge should start extending.
    
    void Update()
    {
        if (shouldExtend)
        {
            // Calculate the difference between the current position and the target position
            float step = extendSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition.position, step);

            // If the bridge reaches or exceeds the target, stop extending.
            if (Vector2.Distance(transform.position, targetPosition.position) < 0.01f)
            {
                shouldExtend = false;
            }
        }
    }

    public void StartExtending()
    {
        shouldExtend = true;
    }
}
*/

 

 
public class BridgeController : MonoBehaviour
{
    public float targetLength;         // The length the bridge should reach when fully extended
    public float extendSpeed = 2.0f;   // Speed at which the bridge extends
    private bool shouldExtend = false; // Flag to check if the bridge should start extending

    private Vector3 initialScale;      // Store the original scale
    private Vector3 initialPosition;   // Store the initial position

    private void Start()
    {
        initialScale = transform.localScale;
        initialPosition = transform.position;
    }

    void Update()
    {
        if (shouldExtend)
        {
            // Calculate the required scale increase
            float scaleIncrease = extendSpeed * Time.deltaTime;

            // Adjust the scale of the bridge
            float newScaleX = transform.localScale.x + scaleIncrease;
            if(newScaleX > targetLength)
            {
                newScaleX = targetLength; // Ensure we don't overshoot
                shouldExtend = false;     // Stop extending once target is reached
            }
            transform.localScale = new Vector3(newScaleX, initialScale.y, initialScale.z);

            // Adjust the position of the bridge to ensure only rightward extension
            transform.position = new Vector3(initialPosition.x + (transform.localScale.x - initialScale.x) / 2, initialPosition.y, initialPosition.z);
        }
    }

    public void StartExtending()
    {
        shouldExtend = true;
    }
}
