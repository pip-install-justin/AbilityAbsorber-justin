using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class glass : MonoBehaviour
{

    //private ParticleSystem explosionEffect;



    // Start is called before the first frame update
    void Start()
    {
        //explosionEffect = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("here");
        if (other.gameObject.CompareTag("ShockwavePlayer"))
        {
            //explosionEffect.Play();
            // Disable the shield
            gameObject.SetActive(false);
        }
    }
}
