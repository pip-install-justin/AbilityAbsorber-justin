using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageToPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI textMeshProUI;
    public GameObject arrow;
    public GameObject FireAbilityInfo;
    public GameObject BatAbilityInfo;
    public GameObject RockAbilityInfo;
    public GameObject GlueAbilityInfo;
    public GameObject StealthAbilityInfo;
    public GameObject ElectricAbilityInfo;
    public GameObject MagnetAbilityInfo;
    public GameObject PlayerDeadMenu;
    void Start()
    {
        textMeshProUI = GetComponent<TextMeshProUGUI>();
    }

    public void DisplayAbilityUnlocked(string new_ability, int slot)
    {
        Debug.Log("unlocked ability");
        textMeshProUI.text = "Unlocked: " + new_ability.ToUpper() + "\nPress " + slot.ToString() + " to equip";
        arrow.SetActive(true);
        if(new_ability=="fire")
        {
            FireAbilityInfo.SetActive(true);
            Invoke("HideFireAbilityInfo", 4f); // Set FireAbilityInfo to inactive after 4 seconds
        }
        if(new_ability=="screech")
        {
            BatAbilityInfo.SetActive(true);
            Invoke("HideBatAbilityInfo", 4f); // Set BatAbilityInfo to inactive after 4 seconds
        }
         if(new_ability=="glue")
        {
            GlueAbilityInfo.SetActive(true);
            Invoke("HideGlueAbilityInfo", 4f); // Set BatAbilityInfo to inactive after 4 seconds
        }
         if(new_ability=="ram")
        {
            RockAbilityInfo.SetActive(true);
            Invoke("HideRockAbilityInfo", 4f); // Set BatAbilityInfo to inactive after 4 seconds
        }
        if(new_ability=="stealth")
        {
            StealthAbilityInfo.SetActive(true);
            Invoke("HideStealthAbilityInfo", 4f); // Set BatAbilityInfo to inactive after 4 seconds
        }
        if(new_ability=="electric")
        {
            ElectricAbilityInfo.SetActive(true);
            Invoke("HideElectricAbilityInfo", 4f); // Set BatAbilityInfo to inactive after 4 seconds
        }
        if(new_ability=="magnet")
        {
            MagnetAbilityInfo.SetActive(true);
            Invoke("HideMagnetAbilityInfo", 4f); // Set BatAbilityInfo to inactive after 4 seconds
        }
        Invoke("Clear", 3f);
        Invoke("HideArrow", 3f); // Set arrow to inactive after 3 seconds
    }

    private void HideFireAbilityInfo()
    {
        FireAbilityInfo.SetActive(false);
    }

    private void HideBatAbilityInfo()
    {
        BatAbilityInfo.SetActive(false);
    }

    private void HideRockAbilityInfo()
    {
        RockAbilityInfo.SetActive(false);
    }
    private void HideGlueAbilityInfo()
    {
        GlueAbilityInfo.SetActive(false);
    }
     private void HideStealthAbilityInfo()
    {
        StealthAbilityInfo.SetActive(false);
    }
     private void HideElectricAbilityInfo()
    {
        ElectricAbilityInfo.SetActive(false);
    }
     private void HideMagnetAbilityInfo()
    {
        MagnetAbilityInfo.SetActive(false);
    }


    public void DisplayDied()
    {
        Debug.Log("player died");
        PlayerDeadMenu.SetActive(true);
        //textMeshProUI.text = "You died\nPress R to Restart\nPress P for pause menu";
        //Invoke("Clear", 3f);
    }

    private void Clear()
    {
        textMeshProUI.text = "";
    }

    private void HideArrow()
    {
        arrow.SetActive(false);
    }
}
