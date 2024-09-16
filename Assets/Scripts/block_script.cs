using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class block_script : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 start_position;
    public GameObject emptyBlocking;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        start_position = transform.position;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        if (!collision.gameObject.CompareTag("ShockwavePlayer") && transform.position == start_position)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("ShockwavePlayer")&& transform.position == start_position)

        {
            rb.velocity = Vector2.zero; 
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("ShockwavePlayer")&& transform.position == start_position)
        {
            rb.constraints &= ~RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ShockwavePlayer"))
        {
            Destroy(emptyBlocking);
        }
    }
}