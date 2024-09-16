using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningHazard : MonoBehaviour
{
    public float rotationSpeed;
    private bool stuck = false;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (stuck == false) {
            float rotationAmount = rotationSpeed * Time.deltaTime;
            transform.Rotate(0f, 0f, rotationAmount);
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        // EVERY ENEMY/OBJECT NEAR GLUE SHOULD HAVE THIS GLUE
        if (other.gameObject.CompareTag("Glue")) {
            Debug.Log("SpinningHazard stuck in glue");
            stuck = true;
        }
    }
}
