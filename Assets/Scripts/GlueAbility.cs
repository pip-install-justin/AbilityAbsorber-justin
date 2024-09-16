using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueAbility : MonoBehaviour
{
    private AbilityManager abilityManager;
    private Rigidbody2D player;
    public GameObject gluePuddlePrefab;

    void Start()
    {
        abilityManager = GetComponent<AbilityManager>();
        player = GetComponent<Rigidbody2D>(); 
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && abilityManager.getSelectedAbility() == "glue")
        {
            Debug.Log("Using glue ability");
            
            // create glue puddle
            Vector2 spawnPosition = transform.position;
            GameObject newfire = Instantiate(gluePuddlePrefab, spawnPosition, Quaternion.identity);
        }
    }
}
