using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AbilityPanelButton : MonoBehaviour
{
    public GameObject abilityTab; // Reference to Abilities Tab
    
    private bool isTabVisible; // To track visibility state of Abilities Tab

    // Create a reference to the EventSystem
    private EventSystem eventSystem;

    void Start()
    {
        // Initially, we assume Abilities Tab is not visible
        isTabVisible = false;
        abilityTab.SetActive(isTabVisible);

        // Initialize the EventSystem
        eventSystem = EventSystem.current;
    }

    public void OnAbilityPanelClick() // Method to be called on button click
    {
        isTabVisible = !isTabVisible; // Toggle visibility state
        abilityTab.SetActive(isTabVisible); // Set the new state

        // Deselect the button to prevent SPACE key from re-activating it
        eventSystem.SetSelectedGameObject(null);
    }
}
