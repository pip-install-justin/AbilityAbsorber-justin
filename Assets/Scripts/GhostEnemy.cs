using System.Collections;
using UnityEngine;

public class GhostEnemy : MonoBehaviour
{
    public float speed = 2f; 
    public GameObject player;
    public float damage = 1.0f;
    private bool isAttacking = false;
    private SpriteRenderer spriteRenderer;
    private PlayerController playerScript;
    private Vector2 direction;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerScript = player.GetComponent<PlayerController>();
        StartCoroutine(ChangeTargetPosition());
    }

    private void Update()
    {
        // Check if something is in the way
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, speed * Time.deltaTime);
        if (hit.collider != null)
        {
            // Change direction
            direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }

        transform.position = (Vector2)transform.position + direction * speed * Time.deltaTime;

        if (isAttacking)
        {
            // Move towards the player
            direction = (player.transform.position - transform.position).normalized;

            // Check for collision with the player
            if (Vector2.Distance(transform.position, player.transform.position) < 0.1f)
            {
                playerScript.TakeDamage(damage,"ghost");
                isAttacking = false;
                spriteRenderer.enabled = false;
                StartCoroutine(ChangeTargetPosition());
            }
        }
    }

    IEnumerator ChangeTargetPosition()
    {
        yield return new WaitForSeconds(4f);
        if (!isAttacking)
        {
            direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            spriteRenderer.enabled = false;
            StartCoroutine(BecomeVisible());
        }
        else
        {
            // Set the ghost to attack mode
            isAttacking = true;
            direction = (player.transform.position - transform.position).normalized;
            spriteRenderer.enabled = true;
        }
    }

    IEnumerator BecomeVisible()
    {
        yield return new WaitForSeconds(Random.Range(1f, 3f));
        spriteRenderer.enabled = true;
        yield return new WaitForSeconds(1f);
        spriteRenderer.enabled = false;
    }
}
