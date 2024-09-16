using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChipTrigger : MonoBehaviour
{
    // Array to hold the LightBulb GameObjects
    public GameObject[] lightBulbs;
    private GameObject electricity;

    // GameObject for the Door
    public GameObject door;

    // Static counter to keep track of how many bulbs are activated
    static int bulbCounter = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if collision is with ElectricAbility
        electricity = GameObject.FindGameObjectWithTag("ElectricAbility");
        if (other.gameObject == electricity)
        {   
            Debug.Log("Activated Chip");

            // Only proceed if there are still LightBulbs left to activate
            if (bulbCounter < lightBulbs.Length) 
            {
                // Activate the next LightBulb and its PointLight child
                lightBulbs[bulbCounter].SetActive(true);
                Debug.Log("Light bulb on " + bulbCounter);
                
                // Increase the bulb counter
                bulbCounter++;
            }

            // Check if all LightBulbs are activated
            if (bulbCounter == lightBulbs.Length) 
            {
                // Deactivate the Door
                door.SetActive(false);
            }
        }
    }
}
