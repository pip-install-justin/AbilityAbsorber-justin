using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    private GameObject dynamite;

    private void Start()
    {
        dynamite = GameObject.Find("Dynamite1");
    }

    private void Update()
    {
        if (dynamite == null)
        {
            print("Dynamite1 no longer in hierarchy");
            print("Barrel removed by fire/explosion");
            Destroy(this.gameObject);
        }
        
    }
}
