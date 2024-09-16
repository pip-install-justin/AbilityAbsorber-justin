using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockEnemy : MonoBehaviour
{

    public GameObject rockOnFire;
    public GameObject rockShield;  // The rock shield game object
    private int currentHealth;
    public GameObject fire;
    public string objectTag = "FireAbility";
    private float health;
    public float maxLives = 3f;
    public Renderer renderer;
    public bool is_corpse = false; 
    
    private Rigidbody2D rb;
    private RockDashAbility dashAbility;
    private PlayerController playerController; //to deal damage to player

    public GameObject Torch1;
    public GameObject Torch2;
    

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        dashAbility = GetComponent<RockDashAbility>();
        health = maxLives;
        rockShield.SetActive(true);
        
    }

    void Update()
    {
        
    }

    

    void OnTriggerEnter2D(Collider2D other)
    {   fire = GameObject.FindGameObjectWithTag(objectTag);
        // If the enemy is hit by a 'screech' or 'fire' tagged object
        // add the implementation of screech ability and respective tag to break the shield
        //fire ability works
        if(other.gameObject.CompareTag("ShockwavePlayer") && rockShield.activeSelf)
        {
            // Disable the rock shield
            rockShield.SetActive(false);
        }
        else if (other.gameObject.CompareTag("FireAbility") && !rockShield.activeSelf)
        {
            
            if (other.gameObject == fire)
            {
                TakeDamage(1f);
            }
            
        }
    }

    public void TakeDamage(float damage)
    {
        if (health > 0)
        {
            health -= damage;
            print(health);
        }
        if (health <=0)
        {
            // Destroy object - gameObject.SetActive(false);

            // make into corpse
            Debug.Log("killed rock");
            Color color = HexToColor("372E2E");
            renderer.material.color = color;
            is_corpse=true;
            if(is_corpse==true)
            {
                // Set the velocity to zero
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
                // Make the Rigidbody2D kinematic, so it's no longer affected by forces
                rb.isKinematic = true;
                // Disable the DashAbility script
                if(dashAbility != null)
                    dashAbility.enabled = false;
                else
                    Debug.LogError("DashAbility script disabled as rock died.");

                
               Torch1.SetActive(true); //make the torche puzzle visible
               Torch2.SetActive(true);

               StartCoroutine(ActivateAndDeactivateFlame());

            }
        }
    }

    IEnumerator ActivateAndDeactivateFlame()
    {
        // Set flame active
        rockOnFire.SetActive(true);

        // Wait for 1 second
        yield return new WaitForSeconds(1f);

        // Set flame inactive
        rockOnFire.SetActive(false);


    }        

    private Color HexToColor(string hex)
    {
        Color color = Color.black;
        ColorUtility.TryParseHtmlString("#" + hex, out color);
        return color;
    }
    

}
