using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
public class  AbilityGuide : MonoBehaviour
{
    // Drag and drop relevant UI components from the inspector.
    public GameObject controlsGuidePanel;
    public GameObject abilityIcon;
    public GameObject healthbar;
    public GameObject controlsPanel;
    public GameObject abilityGuideBanner;
    

    // Reference to the PulsateButton component on "Controls Panel"
    public PulsateButton pulsateButtonScript;
 
    private Button abilityIconButton;

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
        healthbar.SetActive(false);
        controlsPanel.SetActive(false);
        abilityGuideBanner.SetActive(true);
         

        // Set glow variable of the PulsateButton script to true
        pulsateButtonScript.glow = true;
         
    }

    public void ToggleControls()
    {
            abilityGuideBanner.SetActive(false);
            controlsGuidePanel.SetActive(false);
            healthbar.SetActive(true);
            controlsPanel.SetActive(true);
             
            // Set glow variable of the PulsateButton script to false
            pulsateButtonScript.glow = false;
            

        
    }
}
