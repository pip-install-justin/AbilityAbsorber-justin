using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f; // Speed of player movement.
    private Rigidbody2D rb;  // Reference to Rigidbody2D component.

    public bool isMovingLeft;

    // Use this for initialization
    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();
    }

    // FixedUpdate is called once per frame, but with a fixed time increment
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"); // Get horizontal Input
        float moveVertical = Input.GetAxis("Vertical"); // Get vertical Input

        // Create a Vector2 movement vector
        Vector2 movement = new Vector2(moveHorizontal, moveVertical);

        // Apply movement to the Rigidbody2D
        rb.velocity = movement * speed;

        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 1f);
            isMovingLeft = true;
        } 
        else if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            isMovingLeft = false;
        }
    }

    public void Knock(float knockTime)
    { 
        StartCoroutine(KnockCo(knockTime));
    }
    
    private IEnumerator KnockCo(float knockTime)
    {
        if (rb != null)
        {
            yield return new WaitForSeconds(knockTime);
            rb.velocity = Vector2.zero;
            print("works2");
        }
    }

    public void setSpeed(float newspeed) {
        speed = newspeed;
    }

    public float getSpeed()
    {
        return speed;
    }

}

