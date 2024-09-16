using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEnemyRadius : MonoBehaviour
{
    // Start is called before the first frame update

    private PlayerController playerController;
    private AbilityManager abilityManager;

    private void Start()
    {
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        abilityManager = GameObject.FindWithTag("Player").GetComponent<AbilityManager>();

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && abilityManager.getSelectedAbility() != "ram")
        {
            StartCoroutine(DecreaseHealthGradually());
        }
    }
    
    IEnumerator DecreaseHealthGradually()
    {
        playerController.TakeDamage(0.01f ,"fire-enemy");
        yield return new WaitForSeconds(0.01f);
    }
        
}

