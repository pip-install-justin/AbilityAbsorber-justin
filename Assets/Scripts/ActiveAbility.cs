using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ActiveAbility : MonoBehaviour
{
    // Start is called before the first frame update
    TextMeshProUGUI textMeshProUI;
    public GameObject Player;
    private AbilityManager abilityManager;
    public GameObject abilityFire;
    public GameObject abilityBat;
    public GameObject abilityGlue;
    public GameObject abilityRam;
    public GameObject abilityStealth;
    public GameObject abilityElectric;
    public GameObject abilityMagnet;
    //public GameObject HealthBanner;
    private bool batAbilityUnlocked = true;

    void Start()
    {
        textMeshProUI = GetComponent<TextMeshProUGUI>();
        abilityManager = Player.GetComponent<AbilityManager>();
    }

    // Update is called once per frame
    void Update()
    {
        textMeshProUI.text = "Equipped ability: " + abilityManager.getSelectedAbility().ToUpper();
        if(abilityManager.getSelectedAbility()=="fire")
        {
            abilityFire.SetActive(true);
        }
        if(abilityManager.getSelectedAbility()=="screech")
        {
            abilityBat.SetActive(true);
            
        }
        if(abilityManager.getSelectedAbility()=="glue")
        {
            abilityGlue.SetActive(true);
        }
        if(abilityManager.getSelectedAbility()=="ram")
        {
            abilityRam.SetActive(true);
        }
        if(abilityManager.getSelectedAbility()=="stealth")
        {
            abilityStealth.SetActive(true);
        }
        if(abilityManager.getSelectedAbility()=="electric")
        {
            abilityElectric.SetActive(true);
        }
        if(abilityManager.getSelectedAbility()=="magnet")
        {
            abilityMagnet.SetActive(true);
        }
    }
}
