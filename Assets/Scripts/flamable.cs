using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flamable : MonoBehaviour
{
    private GameObject fire;

    private void OnTriggerEnter2D(Collider2D other)
    {
        fire = GameObject.FindGameObjectWithTag("FireAbility");
        if (other.gameObject == fire)
        {
            Destroy(gameObject);
        }
    }
}
