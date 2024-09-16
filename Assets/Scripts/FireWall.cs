using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWall : MonoBehaviour
{
    public GameObject batform; // Reference to Flame GameObject
    public PlayerController playerController; // Reference to PlayerController script

    private void OnTriggerStay2D(Collider2D other)
    {
        // Check if the colliding object is the player
        // Make sure the player gameobject has the tag "Player" in Unity inspector
        if(other.gameObject.tag == "Player")
        {  
            // If the bat is active
            if(batform.activeInHierarchy)
            {
                // Call TakeDamage function from PlayerController script
                playerController.TakeDamage(0.1f,"firewall");
                Debug.Log("firewall dealt damage to player in bat form");
            }
        }
    }
}
