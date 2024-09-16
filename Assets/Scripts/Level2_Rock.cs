using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2_Rock : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed = 2f;             // Adjust the speed of the enemy
    public float jitterRange = 1f;       // Adjust the range of jitter motion

    private Vector2 targetPosition;      // The target position for the next movement
    private Rigidbody2D rb;
    public bool isLeft;
    public float rotationSpeed = 200f;

    private bool is_corpse = false;

    private float health;
    private Vector2 direction;
    public float maxLives = 3f;
    public Renderer renderer;
    public GameObject player;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxLives;
    }

    private void Start()
    {
        // Set the initial target position
        renderer = GetComponent<Renderer>();
    }

    bool canMove = true;

    IEnumerator WaitForSecondsCoroutine(float seconds)
    {
        canMove = false;
        yield return new WaitForSeconds(seconds);
        canMove = true;
    }

    private void Update()
    {
        if (is_corpse == false && canMove == true)
        {
            Vector2 player_position = player.transform.position;
            if (Vector2.Distance(transform.position, player_position) <= 10f)
            {
                direction = (player_position - (Vector2)transform.position).normalized;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, LayerMask.GetMask("Obstacle"));
                if (hit.collider != null)
                {
                    // Calculate a new direction away from the wall
                    direction = Vector2.Reflect(direction, hit.normal);
                    StartCoroutine(WaitForSecondsCoroutine(0.5f)); // 
                    //print("direction 2: " + direction);
                }

                rb.velocity = direction * speed;

            }
            else
            {
                rb.velocity = new Vector2(0, 0);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Explosion"))
        {
            print("rock killed by explosion");
            TakeDamage(maxLives);
        }
        else if (other.gameObject.CompareTag("ShockwavePlayer"))
        {
            speed = 2f;
            print("freed rock from glue using screech");
            print("rock shattered by mega screech");
            if (player.GetComponent<screech_ability>().getIsMegaScreech())
            {
                TakeDamage(maxLives);
            }
        }
        else if (other.gameObject.CompareTag("FireAbility"))
        {
            speed = 2f; //unsticks from glue
            print("freed rock from glue using fire");
        }

        // EVERY ENEMY NEAR GLUE SHOULD HAVE THIS GLUE
        else if (other.gameObject.CompareTag("Glue"))
        {
            Debug.Log("Rock stuck in glue");
            speed = 0.3f;
        }
    }


    public void TakeDamage(float damage)
    {
        // reset speed
        speed = 2f;

        if (health > 0)
        {
            health -= damage;
            print(health);
        }
        if (health <= 0)
        {
            // Destroy object - gameObject.SetActive(false);

            // make into corpse
            Debug.Log("killed rock");
            is_corpse = true;
            rb.velocity = new Vector2(0f, 0f);
            Color color = HexToColor("372E2E");
            renderer.material.color = color;
        }

    }

    public bool getIsCorpse()
    {
        return is_corpse;
    }

    private Color HexToColor(string hex)
    {
        Color color = Color.black;
        ColorUtility.TryParseHtmlString("#" + hex, out color);
        return color;
    }
}