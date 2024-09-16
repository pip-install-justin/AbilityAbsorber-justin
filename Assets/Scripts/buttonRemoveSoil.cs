using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonRemoveSoil : MonoBehaviour
{
    public GameObject soil;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("destroying soil");
        Destroy(soil.gameObject);   
    }
}
