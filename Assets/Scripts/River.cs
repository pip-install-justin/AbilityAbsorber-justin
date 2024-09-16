using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class River : MonoBehaviour
{
    public GameObject flame; 
    public GameObject electricform;// Reference to Flame GameObject
    public PlayerController playerController; // Reference to PlayerController script

    private void OnTriggerStay2D(Collider2D other)
    {
        // Check if the colliding object is the player
        // Make sure the player gameobject has the tag "Player" in Unity inspector
        if(other.gameObject.tag == "Player")
        { 
            // If the flame is active
            if(flame.activeInHierarchy)
            {
                // Call TakeDamage function from PlayerController script
                playerController.TakeDamage(0.1f,"river");
                Debug.Log("river dealt damage to player in fire form");
            }

            if(electricform.activeInHierarchy)
            {
                // Call TakeDamage function from PlayerController script
                playerController.TakeDamage(0.7f,"river");
                Debug.Log("river dealt damage to player in electricity form");
            }
        }
    }
}
*/

 

public class River : MonoBehaviour
{
    public GameObject flame;
    public GameObject electricform;
    public PlayerController playerController;
    
  

    private void OnTriggerStay2D(Collider2D other)
    {
        // Check if the colliding object is the player
        if (other.gameObject.tag == "Player")
        {
            // If the flame is active
            if (flame.activeInHierarchy)
            {
                playerController.TakeDamage(0.01f, "river");
                Debug.Log("river dealt damage to player in fire form");
            }

            // If electric form is active and player is NOT on the bridge
            if (electricform.activeInHierarchy && !playerController.isOnBridge)
            {
                playerController.TakeDamage(0.04f, "river");
                Debug.Log("river dealt damage to player in electricity form");
            }
        }
    }

    
}
