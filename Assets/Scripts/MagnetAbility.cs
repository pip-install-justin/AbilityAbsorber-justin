using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetAbility : MonoBehaviour
{
    public float attractionForce;
    public float duration = 0.5f;
    private AbilityManager abilityManager;
    private Rigidbody2D player;
    public GameObject magnetRadiusPrefab;
    public float radius; // IF YOU CHANGE THIS, CHANGE THE MAGNETRADIUSPREFAB SIZE TOO

    private GameObject newradius;
 

    void Start()
    {
        abilityManager = GetComponent<AbilityManager>();
        player = GetComponent<Rigidbody2D>();
    }


    // for circle
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && abilityManager.getSelectedAbility() == "magnet")
        {
            Debug.Log("Using magnet radius ability");

            // create circle magnet radius
            Vector2 spawnPosition = transform.position;
            newradius = Instantiate(magnetRadiusPrefab, spawnPosition, Quaternion.identity);
            newradius.transform.parent = transform; // setting it to follow player
            Destroy(newradius, duration);

            



        }
    }

    // for attraction
    void FixedUpdate()
    {
        if (newradius != null && !ReferenceEquals(newradius, null)) {
            Attract("Metal", attractionForce);
            Attract("FireEnemy", attractionForce * 2);
        }
        
    }

    private void Attract(string tag, float force) {
        Vector2 currentPosition = transform.position;
        GameObject[] metalObjects = GameObject.FindGameObjectsWithTag(tag);
        foreach (GameObject metalObject in metalObjects)
        {
            Vector2 metalObjectPosition = metalObject.transform.position;
            float distance = Vector2.Distance(currentPosition, metalObjectPosition);
            //print(distance);
            if (distance < radius)
            {
                if (metalObject.name == "Metal_Block")
                    metalObject.GetComponent<Rigidbody2D>().mass = 1f;
                //print("found metal object to attract");
                Vector2 direction = currentPosition - metalObjectPosition;
                Rigidbody2D rb = metalObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                    rb.AddForce(direction.normalized * attractionForce);
            }
        }
    }

}
