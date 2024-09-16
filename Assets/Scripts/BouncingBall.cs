using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BouncingBall : MonoBehaviour
{
    public float speed = 5f; // Speed of the circle sprite
    public float maxX = 10f; // Maximum X position of the room
    public float minX = -10f; // Minimum X position of the room
    public float maxY = 5f; // Maximum Y position of the room
    public float minY = -5f; // Minimum Y position of the room
    private bool isCollidingWithGlue = false;
    private Vector2 targetPosition;
    private Rigidbody2D rb;
    private bool hasCollided = false;
    private int count=0; 

    void Start()
    {
        targetPosition = GetRandomPosition();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Move towards the target position
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // If the target position is reached, get a new random position
        if ((Vector2)transform.position == targetPosition)
        {
            targetPosition = GetRandomPosition();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Change direction upon collision only once
        if (!hasCollided)
        {
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
            targetPosition = (Vector2)transform.position - direction * speed * Time.deltaTime;
            hasCollided = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Glue") && other.gameObject.name != "GlueBottle")
        {
            Debug.Log("Ball stuck in glue");
            isCollidingWithGlue = true;
            speed = 0f;
            rb.velocity = Vector2.zero;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            count++;
        }
        if(count>=1)
        {
            GameObject closed_door = GameObject.Find("Closed door");
            Destroy(closed_door);
        }
    }

     
    void OnCollisionExit2D(Collision2D collision)
    {
        // Reset collision flag when no longer in contact with an object
        hasCollided = false;
    }

    Vector2 GetRandomPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        return new Vector2(randomX, randomY);
    }
}
