using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeSwitchController : MonoBehaviour
{
    // Start is called before the first frame update
    private BridgeController bridgeController;

    private void Start()
    {
        bridgeController = FindObjectOfType<BridgeController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ElectricAbility"))
        {   
            bridgeController.StartExtending();
        }
    }
}
