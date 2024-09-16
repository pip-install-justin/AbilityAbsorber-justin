using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTorch : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject torchFire;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

void OnTriggerEnter2D(Collider2D other)
{
    if (other.gameObject.CompareTag("FireAbility"))
    {
        torchFire.SetActive(true);
        Debug.Log("Torch lit up");
    }
}

}
