using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    public float damage = 1f;
    public float duration = 0.5f;
    public float period = 2f;  // Time interval between each fire
    public GameObject ThunderPrefab;
    public Sprite brokenEletronic; // Drag and drop your new sprite here in inspector
    public GameObject player; // Reference to player gameobject
    public float proximityThreshold = 5f; // distance threshold for sprite change and stop thunder

    private SpriteRenderer spriteRenderer;
    private Sprite originalSprite;
    public bool isBroken = false;

    private AbilityManager abilityManager;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalSprite = spriteRenderer.sprite;
        abilityManager = player.GetComponent<AbilityManager>();

        // Start the fire ability coroutine
        StartCoroutine(ThunderCoroutine());
    }

    void Update()
    {
        // Check if player is close enough
        if (!isBroken && Vector2.Distance(player.transform.position, transform.position) <= proximityThreshold)
        {
            if (abilityManager.getSelectedAbility() == "ram" || abilityManager.getSelectedAbility() == "stealth") {
                // Change sprite
                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = brokenEletronic;
                }
                isBroken = true; // set the generator to broken state
            }
        }
    }

    IEnumerator ThunderCoroutine()
    {
        while (true)
        {
            // Check if player is far enough and the generator is not broken
            if (!isBroken && Vector2.Distance(player.transform.position, transform.position) > proximityThreshold)
            {
                //Debug.Log("Using Thunder radius ability");

                // create circle fireball radius
                Vector2 spawnPosition = transform.position;
                GameObject newfire = Instantiate(ThunderPrefab, spawnPosition, Quaternion.identity);
                newfire.transform.parent = transform; // setting it to follow object
                Destroy(newfire, duration);
            }

            // Wait for the next fire
            yield return new WaitForSeconds(duration + period);
        }
    }
}
