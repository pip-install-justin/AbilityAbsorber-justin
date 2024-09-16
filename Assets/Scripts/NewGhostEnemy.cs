using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewGhostEnemy : MonoBehaviour
{
    private float maxLives = 3f;
    public float roamDuration = 4f;
    public float dashSpeed = 4f;
    public float roamSpeed = 2f;
    public GameObject player;
    public Vector2 roamAreaMin, roamAreaMax;
    public bool is_corpse = false;
    private float health;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Vector2 roamPosition;
    private AbilityManager abilityManager;
    private RockAbility rockAbility;
    public GameObject tombstonePrefab;
    private bool isCollidingWithGlue = false; // Added variable
    private ShowDamage showDamage;
    public GameObject exit_closedDoor;
    public GameObject exit_openDoor;

    void Start()
    {
        health = maxLives;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        abilityManager = GetComponent<AbilityManager>();
        rockAbility = GetComponent<RockAbility>();
        spriteRenderer.enabled = false;
        StartCoroutine(GhostStateMachine());
        showDamage = GetComponent<ShowDamage>();
    }

    IEnumerator GhostStateMachine()
    {
        while (true)
        {
            yield return Roam();
            yield return Dash();
        }
    }

    IEnumerator Roam()
    {
        spriteRenderer.enabled = false;
        roamPosition = new Vector2(
            Random.Range(roamAreaMin.x, roamAreaMax.x),
            Random.Range(roamAreaMin.y, roamAreaMax.y)
        );

        float startTime = Time.time;
        while (Time.time < startTime + roamDuration)
        {
            Vector2 direction = (roamPosition - (Vector2)transform.position).normalized;
            rb.velocity = direction * roamSpeed;
            yield return null;
        }
    }

    IEnumerator Dash()
    {
        spriteRenderer.enabled = true;
        Vector2 direction = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;
        rb.velocity = direction * dashSpeed;
        yield return new WaitForSeconds(1);
    }

    public void TakeDamage(float damage)
    {
        if (health > 0)
        {
            showDamage.TurnRed();
            health -= damage;
            Debug.Log("Ghost health: " + health);
        }

        if (health <= 0)
        {
            Debug.Log("Ghost killed");
            is_corpse = true;
            rb.velocity = new Vector2(0f, 0f);
            spriteRenderer.enabled = false;
            this.enabled = false; // disable this script
            // Spawn tombstone
            if (tombstonePrefab != null)
            {
                Instantiate(tombstonePrefab, transform.position, Quaternion.identity);
            }

            // Disable the ghost object
            gameObject.SetActive(false);
            //open the door sprite
            exit_closedDoor.SetActive(false);
            exit_openDoor.SetActive(true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Glue"))
        {
            Debug.Log("Ghost stuck in glue");
            isCollidingWithGlue = true;
            dashSpeed = 0f;
            roamSpeed = 0f;
            spriteRenderer.enabled = true;
            
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Glue"))
        {
            Debug.Log("Ghost no longer stuck in glue");
            isCollidingWithGlue = false;
            dashSpeed = 5f;
            roamSpeed = 2f;
            spriteRenderer.enabled = false;
            
        }
    }


    public bool getIsCorpse()
    {
        return is_corpse;
    }
}
