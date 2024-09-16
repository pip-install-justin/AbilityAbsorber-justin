using UnityEngine;

public class BatShockWave : MonoBehaviour
{
    public GameObject shockWave;
    public float minSpawnDelay = 6f;
    public float maxSpawnDelay = 9f;
    public Transform shockWaveSpawn;
    public Transform playerTransform;
    private float spawnTimer;
    private float currentSpawnDelay;
    private GameObject waveObject;
    private Bat batScript;
    public PlayerMovement playerMovement;
    private void Start()
    {
        // Set the initial timer and spawn delay
        SetRandomSpawnDelay();
        spawnTimer = currentSpawnDelay;
        batScript = GetComponent<Bat>();
    }

    private void Update()
    {
        if (batScript.getIsCorpse() == false) {

            // Check if the timer has reached zero
            if (spawnTimer <= 0)
            {
                // Spawn the prefab in front of the enemy
                Vector3 spawnPosition = shockWaveSpawn.position;
                waveObject = Instantiate(shockWave, spawnPosition, Quaternion.identity);
                // Reset the timer and set a new spawn delay
                SetRandomSpawnDelay();
                spawnTimer = currentSpawnDelay;
            }
            else
            {
                // Decrease the timer
                spawnTimer -= Time.deltaTime;
            }

            if (waveObject != null)
            {
                waveObject.transform.position = shockWaveSpawn.position;
                if (!batScript.isLeft)
                {
                    // Flip the sprite to face right
                    waveObject.transform.localScale = new Vector3(4f, 4f, 1f);
                }
                else
                {
                    // Flip the sprite to face left
                    waveObject.transform.localScale = new Vector3(-4f, 4f, 1f);
                }
                Destroy(waveObject, 3f);
            }
        }
    }

    private void SetRandomSpawnDelay()
    {
        // Calculate a random spawn delay within the specified range
        currentSpawnDelay = Random.Range(minSpawnDelay, maxSpawnDelay);
    }
}
