/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlPanelButton : MonoBehaviour
{
    public GameObject controlTab; // Reference to Controls Tab
    
    private bool isTabVisible; // To track visibility state of Controls Tab
    
    void Start()
    {
        // Initially, we assume Controls Tab is not visible
        isTabVisible = false;
        controlTab.SetActive(isTabVisible);
    }

    public void OnControlPanelClick() // Method to be called on button click
    {
        isTabVisible = !isTabVisible; // Toggle visibility state
        controlTab.SetActive(isTabVisible); // Set the new state
    }
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ControlPanelButton : MonoBehaviour
{
    public GameObject controlTab; // Reference to Controls Tab
    
    private bool isTabVisible; // To track visibility state of Controls Tab

    // Create a reference to the EventSystem
    private EventSystem eventSystem;

    void Start()
    {
        // Initially, we assume Controls Tab is not visible
        isTabVisible = false;
        if (controlTab != null)
            controlTab.SetActive(isTabVisible);

        // Initialize the EventSystem
        eventSystem = EventSystem.current;
    }

    public void OnControlPanelClick() // Method to be called on button click
    {
        isTabVisible = !isTabVisible; // Toggle visibility state
        if (controlTab != null)
            controlTab.SetActive(isTabVisible); // Set the new state

        // Deselect the button to prevent SPACE key from re-activating it
        eventSystem.SetSelectedGameObject(null);
    }
}
