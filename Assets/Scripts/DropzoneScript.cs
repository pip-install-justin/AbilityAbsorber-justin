using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropzoneScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject generator;
    public GameObject placedGenerator;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Metal"))
        {
            Destroy(generator);
            //Instantiate(placedGenerator, transform.position, Quaternion.identity);
            placedGenerator.SetActive(true);
        }
    }
}
