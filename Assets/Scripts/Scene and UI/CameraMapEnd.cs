using System.Collections;
using UnityEngine;

public class CameraMapEnd : MonoBehaviour
{
    public Vector3 startPosition;  // Starting position of the camera
    public Vector3 endPosition;  // End position of the camera
    public float speed = 1f;  // Speed of the camera movement
    public float delay = 2f;  // Delay at the end points

    void Start()
    {
        // Move to the end of the map and back
        StartCoroutine(MoveCamera());
    }

    IEnumerator MoveCamera()
    {
        // Move to the end position
        yield return StartCoroutine(MoveBetweenPositions(startPosition, endPosition));

        // Wait for some time at the end
        yield return new WaitForSeconds(delay);

        // Move back to the start position
        yield return StartCoroutine(MoveBetweenPositions(endPosition, startPosition));

        // Wait for some time at the start
        yield return new WaitForSeconds(delay);
    }

    IEnumerator MoveBetweenPositions(Vector3 start, Vector3 end)
    {
        float journeyLength = Vector3.Distance(start, end);
        float startTime = Time.time;

        while (Vector3.Distance(transform.position, end) > 0.1f)
        {
            // Calculate distance moved = time * speed
            float distCovered = (Time.time - startTime) * speed;
            // Fraction of journey completed = current distance / total distance
            float fracJourney = distCovered / journeyLength;
            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(start, end, fracJourney);
            yield return null;
        }
    }
}
