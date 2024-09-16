using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GluePuddle : MonoBehaviour
{
    // things stuck in glue:
    // rock enemy(done)
    // spinning hazard (done)
    // ghost enemy (done)
    // basketball (done)


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
        if (other.gameObject.CompareTag("Explosion") || other.gameObject.CompareTag("FireAbility") || other.gameObject.CompareTag("ShockwavePlayer")) {
            if (gameObject.name != "GlueBottle") { //if self is bottle, don't remove bottle
                print("glue removed by fire/explosion");
                Destroy(this.gameObject);
            }
        }
        
    }
}
