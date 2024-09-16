using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class FireEnemy : MonoBehaviour
{
    public float speed = 5f;             // Adjust the speed of the enemy
    public float jitterRange = 10f;       // Adjust the range of jitter motion

    private Vector2 targetPosition;      // The target position for the next movement
    private Rigidbody2D rb;
    public PlayerController playerController;
    private float spawnTimer;
    private float currentSpawnDelay;
    public GameObject radiusFlame;
    public float minSpawnDelay = 7f;
    public float maxSpawnDelay = 10f;

    public float radiusFlameDuration = 2f;
    // Start is called before the first frame update
    void Start()
    {
        SetRandomSpawnDelay();
        spawnTimer = currentSpawnDelay;
        rb = GetComponent<Rigidbody2D>();
    }

    // Check if the enemy has reached the target position
    void Update()
    {
        if (Vector2.Distance(transform.position, targetPosition) <= 0.1f)
        {
            // Get a new random target position
            targetPosition = GetRandomPosition();
        }

        // Move towards the target position
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        // Avoid walls
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, LayerMask.GetMask("Obstacle"));
        if (hit.collider != null)
        {
            // Calculate a new direction away from the wall
            direction = Vector2.Reflect(direction, hit.normal);
            //print("direction 2: " + direction);
            targetPosition = GetRandomPosition();
        }// Check if the enemy has reached the target position
        rb.velocity = direction * speed;
        
        if (spawnTimer <= 0)
        {
            Vector2 spawnPosition = transform.position;
            GameObject newfire = Instantiate(radiusFlame, spawnPosition, Quaternion.identity);
            newfire.transform.parent = transform; // setting it to follow enemy
            Destroy(newfire, radiusFlameDuration);
            SetRandomSpawnDelay();
            spawnTimer = currentSpawnDelay;
        }
        else
        {
            // Decrease the timer
            spawnTimer -= Time.deltaTime;
        }
    }
    
    private Vector2 GetRandomPosition()
    {
        // Generate a random position within the jitter range
        Vector2 randomOffset = Random.insideUnitCircle * jitterRange;
        return (Vector2)transform.position + randomOffset;
    }
    
    private void SetRandomSpawnDelay()
    {
        // Calculate a random spawn delay within the specified range
        currentSpawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
    }

   
}


