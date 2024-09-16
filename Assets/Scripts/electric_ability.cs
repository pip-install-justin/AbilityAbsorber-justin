using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class electric_ability : MonoBehaviour
{
    public float damage = 1f;
    public float duration = 0.5f;
    private AbilityManager abilityManager;
    private Rigidbody2D player;
    public GameObject ThunredPrefab;

    void Start()
    {
        abilityManager = GetComponent<AbilityManager>();
        player = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && abilityManager.getSelectedAbility() == "electric")
        {
            Debug.Log("Using electric radius ability");

            Vector2 spawnPosition = transform.position;
            GameObject newfire = Instantiate(ThunredPrefab, spawnPosition, Quaternion.identity);
            newfire.transform.parent = transform; // setting it to follow player
            Destroy(newfire, duration);

        }
    }
}



