using UnityEngine;
using UnityEngine.SearchService;

public class Bat : MonoBehaviour
{
    public float speed = 2f;             // Adjust the speed of the enemy
    public float jitterRange = 1f;       // Adjust the range of jitter motion

    private Vector2 targetPosition;      // The target position for the next movement
    private Rigidbody2D rb;
    public Transform playerTransform;
    public bool isLeft;

    private bool is_corpse = false; 

    private float health;
    public float maxLives = 3f;
    private SpriteRenderer renderer;
    Transform glassShield;
    private ShowDamage damageScript;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxLives;
        damageScript = GetComponent<ShowDamage>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        // Set the initial target position
        targetPosition = GetRandomPosition();
        renderer = GetComponent<SpriteRenderer>();
        glassShield = transform.Find("Glass");
    }

    private void Update()
    {
        if (is_corpse == false) {

            // Check if the enemy has reached the target position
            if (Vector2.Distance(transform.position, targetPosition) <= 0.1f)
            {
                // Get a new random target position
                targetPosition = GetRandomPosition();
            }

            // Move towards the target position
            Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
            //print("direction 1: " + direction);
            // Avoid walls
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, LayerMask.GetMask("Obstacle"));
            if (hit.collider != null)
            {
                // Calculate a new direction away from the wall
                direction = Vector2.Reflect(direction, hit.normal);
                //print("direction 2: " + direction);
                targetPosition = GetRandomPosition();
            }

            rb.velocity = direction * speed;
            
            //flip player sprite towards player
            if (playerTransform != null)
            {
                
                if (transform.position.x < playerTransform.position.x)
                {
                    // Flip the sprite to face right
                    transform.localScale = new Vector3(5f, 5f, 1f);
                    isLeft = false;
                    //print(isLeft);
                }
                else
                {
                    // Flip the sprite to face left
                    transform.localScale = new Vector3(-5f, 5f, 1f);
                    isLeft = true;
                    //print(isLeft);
                }
            }
        }
    }

    private Vector2 GetRandomPosition()
    {
        // Generate a random position within the jitter range
        Vector2 randomOffset = Random.insideUnitCircle * jitterRange;
        return (Vector2)transform.position + randomOffset;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("FireAbility")) {
            TakeDamage(1f);
        }
    }

    public void TakeDamage(float damage)
    {
        if (glassShield == null || !glassShield.gameObject.activeSelf) {
            if (health > 0)
            {
                health -= damage;
                print(health);
                damageScript.TurnRed();
            }
            if (health <=0)
            {
                // Destroy object - gameObject.SetActive(false);

                // make into corpse
                Debug.Log("killed bat");
                is_corpse = true;
                rb.velocity = new Vector2(0f, 0f);
                Color color = HexToColor("372E2E");
                renderer.material.color = color;
                animator.enabled = false;
            }
        }
    }

    public bool getIsCorpse() {
        return is_corpse;
    }

    private Color HexToColor(string hex)
    {
        Color color = Color.black;
        ColorUtility.TryParseHtmlString("#" + hex, out color);
        return color;
    }
}

