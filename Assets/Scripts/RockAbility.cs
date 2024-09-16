using System.Collections;
using UnityEngine;

public class RockAbility : MonoBehaviour
{
    private AbilityManager abilityManager;
    private PlayerMovement playerMovement;
    public float rushSpeed = 20f;  // The temporary speed of the rush
    public float rushDuration = 0.5f; // The duration of the rush
    public bool isRushing = false;
    private float normalSpeed;

    void Start()
    {
        abilityManager = GetComponent<AbilityManager>();
        playerMovement = GetComponent<PlayerMovement>();
        normalSpeed = playerMovement.getSpeed(); // Assuming you have a way to get player's normal speed
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && abilityManager.getSelectedAbility() == "ram" && !isRushing)
        {
            Debug.Log("Using rock ability");
            StartCoroutine(RushCoroutine());
        }
    }

    IEnumerator RushCoroutine()
    {
        isRushing = true;
        playerMovement.setSpeed(rushSpeed); // Increase player speed temporarily
        yield return new WaitForSeconds(rushDuration); // Wait for the rush duration
        playerMovement.setSpeed(normalSpeed); // Reset player speed to normal
        isRushing = false;
    }
}
