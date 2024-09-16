using System;
using System.Collections;
using UnityEngine;

public class pushback_and_stun : MonoBehaviour
{
    public string objectTag = "Shockwave";
    public float pushForce = 200f;

    private GameObject shockWave;
    private GameObject shockWavePlayer;
    private int counter = 0;
    private int pushTime = 2;
    PlayerMovement playerMovement;
    private PlayerController playerController;
    private bool takingDamage;

    private void Start()
    {
        takingDamage = false;
        playerMovement = GetComponent<PlayerMovement>();
        playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject [] shockWaves = GameObject.FindGameObjectsWithTag(objectTag);
        foreach (GameObject shockWave in shockWaves)
        {
            if (other.gameObject == shockWave)
            {
                print("shockwave collision");
                if (gameObject.CompareTag("Player"))
                {
                    if (!takingDamage)
                        StartCoroutine(DamageAndShortStun());
                }

            }
        } 
        if (other.gameObject == shockWavePlayer)
        {
            if (gameObject.CompareTag("ScreechBlock"))
            {
                
            }
            else if (gameObject.CompareTag("Enemy"))
            {
                print("works");
                //TODO: add once rock enemy 
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
       
        GameObject [] shockWaves = GameObject.FindGameObjectsWithTag(objectTag);
        shockWavePlayer = GameObject.FindGameObjectWithTag("ShockwavePlayer");
        foreach (GameObject shockWave in shockWaves)
        {
            if (other.gameObject == shockWave && !gameObject.CompareTag("ScreechBlock"))
            {
                //print("doesnt work");
                Vector2 dir = other.transform.position - transform.position;
                // We then get the opposite (-Vector3) and normalize it
                dir = -dir.normalized;
                if (GetComponent<Rigidbody2D>() != null)
                {
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    GetComponent<Rigidbody2D>().AddForce(dir * pushForce);
                }
            }
        }

        if (gameObject.CompareTag("ScreechBlock"))
        {
            if (other.gameObject == shockWavePlayer)
            {
                //print("works well");
                Vector2 dir = other.transform.position - transform.position;
                // We then get the opposite (-Vector3) and normalize it
                dir = -dir.normalized;
                if (GetComponent<Rigidbody2D>() != null)
                {
                    GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    GetComponent<Rigidbody2D>().AddForce(dir*pushForce);
                }
            }   
        }
    }

    private IEnumerator DamageAndShortStun()
    {
        takingDamage = true;
        playerController.TakeDamage(0.5f, "bat");
        playerMovement.enabled = false;
        print("triggered");
        yield return new WaitForSeconds(1.2f);
        takingDamage = false;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        if (gameObject.CompareTag("Player"))
        {
            playerMovement.enabled = true;
        } 
        else if (gameObject.CompareTag("Enemy"))
        {
            //TODO: add once rock enemy
        }
    }
    

}

