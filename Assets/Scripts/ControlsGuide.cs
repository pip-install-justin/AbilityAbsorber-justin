using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
public class  ControlsGuide : MonoBehaviour
{
    // Drag and drop relevant UI components from the inspector.
    public GameObject controlsGuidePanel;
    public GameObject controlsIcon;
    public GameObject activeAbility;
    public GameObject abilityPanel;
    public GameObject controlsBanner;
     

    // Reference to the PulsateButton component on "Controls Panel"
    public PulsateButton pulsateButtonScript;
 
    private Button controlsIconButton;

    /*
    private void Awake()
    {
        // Assuming the "Controls Icon" is a button, get its Button component
        controlsIconButton = controlsIcon.GetComponent<Button>();
        controlsIconButton.onClick.AddListener(ToggleControls);
    }*/
     

    private void OnTriggerEnter(Collider other)
    {  
        // Assuming your player has a tag named "Player". Adjust if necessary.
        /*if (other.gameObject.tag == "Player")
        {   Debug.Log("Player collided with controls guide trigger");
            ShowControls();
            Debug.Log("Player collided with controls guide trigger");
        }*/
    }

    public void ShowControls()
    {
        controlsGuidePanel.SetActive(true);
        activeAbility.SetActive(false);
        abilityPanel.SetActive(false);
        controlsBanner.SetActive(true);

        // Set glow variable of the PulsateButton script to true
        pulsateButtonScript.glow = true;
         
    }

    public void ToggleControls()
    {
            controlsBanner.SetActive(false);
            controlsGuidePanel.SetActive(false);
            activeAbility.SetActive(true);
            abilityPanel.SetActive(true);
            
            // Set glow variable of the PulsateButton script to false
            pulsateButtonScript.glow = false;
        
    }
}
