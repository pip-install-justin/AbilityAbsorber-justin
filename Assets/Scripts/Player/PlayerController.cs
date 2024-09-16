using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public TextMeshProUGUI healthLabel;
    public float health;
    public float batHealth;
    public float maxLives;
    public SendToGoogle sendtogoogle;
    public MessageToPlayer messageToPlayer;
    public bool isBat;
    private GameObject enemy;
    private Level2_Rock rockEnemy;
    private NewGhostEnemy ghostEnemy;
    public PauseMenuController pmc;
    private RockAbility rockAbility;
    public PlayerMovement playerMov;
    private AbilityManager abilityManager;
    public Healthbar healthBarScript;
    public bool isOnBridge = false; // Bool flag to track if the player is on the bridge
    public ControlsGuide controlsGuide;
    public AbilityGuide abilityGuide;
    // Start is called before the first frame update
    void Awake()
    {
        isBat = false;
        health = maxLives;
        ///healthLabel.text = "Health: " + health;
        gameObject.SetActive(true);
        sendtogoogle = GetComponent<SendToGoogle>();
        enemy = GameObject.FindGameObjectWithTag("RockEnemy");
        if (enemy != null)
        {
            rockEnemy = enemy.GetComponent<Level2_Rock>();
        }

        if (GameObject.FindGameObjectWithTag("GhostEnemy") != null)
        {
            ghostEnemy = GameObject.FindGameObjectWithTag("GhostEnemy").GetComponent<NewGhostEnemy>();
        }
        rockAbility = GetComponent<RockAbility>();
        abilityManager = GetComponent<AbilityManager>();
    }

    public float getHealthy()
    {
        return health;
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("RockEnemy") && rockEnemy.getIsCorpse() == false && abilityManager.getSelectedAbility() != "glue")
        {
            TakeDamage(1f, "rock");
        }
        if (collision.gameObject.CompareTag("GhostEnemy") && ghostEnemy.getIsCorpse() == false && rockAbility.isRushing == false )
        {   Debug.Log("Ghost dealt damage to player");
            TakeDamage(1f, "ghost");
        }
        if (collision.gameObject.CompareTag("GhostEnemy")  && ghostEnemy.getIsCorpse() == false && abilityManager.getSelectedAbility() == "ram" && rockAbility.isRushing == true)
        {    
            Debug.Log("Player dealt damage to ghost by ramming");
            ghostEnemy.TakeDamage(1.0f);
        }
        if (collision.gameObject.CompareTag("Diamond")  &&  abilityManager.getSelectedAbility() == "ram" && rockAbility.isRushing == true)
        {    
            Debug.Log("Player took damage by ramming into diamond");
            TakeDamage(0.5f,"Diamond");
        }
        if (collision.gameObject.CompareTag("SpinningHazard")) {
            TakeDamage(3f, "SpinningHazard");
        }
        if (collision.gameObject.CompareTag("Magnet")) {
            if (abilityManager.getSelectedAbility() == "electric" && collision.gameObject.GetComponent<MagnetEnemy>().getIsAlive() == true) {
                TakeDamage(3f, "Magnet");
            }
        }
    }

    private int clickCount = 0; // To count the clicks
    private int selection;

    // Add flags at the class level to track collisions
    private bool hasCollidedWithControlsGuide = false;
    private bool hasCollidedWithAbilityGuide = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("FireEnemy") && abilityManager.getSelectedAbility() != "ram") 
        {
            TakeDamage(1f, "fire-enemy");
        }
        
        // Check collision with ControlsGuide and if it hasn't collided before
        if (other.gameObject.tag == "ControlsGuide" && !hasCollidedWithControlsGuide)
        {   
            Debug.Log("Player collided with controls guide trigger");
            controlsGuide.ShowControls();
            selection = 1;
            
            // Mark that the collision has happened
            hasCollidedWithControlsGuide = true;

            // Start listening for button clicks
            StartCoroutine(CheckForDoubleClick());
        }

        // Check collision with AbilityGuide and if it hasn't collided before
        if (other.gameObject.tag == "AbilityGuide" && !hasCollidedWithAbilityGuide)
        {   
            Debug.Log("Player collided with ability guide trigger");
            abilityGuide.ShowControls();
            selection = 2;
            
            // Mark that the collision has happened
            hasCollidedWithAbilityGuide = true;

            // Start listening for button clicks
            StartCoroutine(CheckForDoubleClick());
        }
    }

    private IEnumerator CheckForDoubleClick()
    {
        // Reset click count
        clickCount = 0;
        
        // Add a listener to the mouse button click
        // This assumes you're using the left mouse button; change 0 to 1 for right button
        while (clickCount < 2)
        {
            if (Input.GetMouseButtonDown(0))
            {
                clickCount++;
                controlsGuide.controlsBanner.SetActive(false);
                abilityGuide.abilityGuideBanner.SetActive(false);
            }
            yield return null;
        }

        // If we got here, two clicks were registered
        if(selection==1)
        {controlsGuide.ToggleControls();}
        if(selection == 2)
        {abilityGuide.ToggleControls();}
    }

    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Water") && abilityManager.getSelectedAbility() == "electric")
        {
            float damage = 0.01f * Time.deltaTime;
            TakeDamage(damage, "water");        
        }
        // Check if the player enters the collider of an object with the "bridge" tag
        if (other.gameObject.CompareTag("Bridge"))
        {
            isOnBridge = true;
            Debug.Log("Player in contact with bridge");
        }
    }
    
    void Update()
    {

    }

    public void TakeDamage(float damage, string enemy)
    {
        
        if (enemy == "self-stealth" || GetComponent<GhostAbility>().getUsingStealth() == false) {
            if (health > 0)
            {
                ShowDamage[] showDamages = GetComponentsInChildren<ShowDamage>();
                foreach(ShowDamage showDamage in showDamages)
                {
                    showDamage.TurnRed();
                    
                }
                healthBarScript.FlashBar();
                health -= damage;
            }
            if (health<= 0)
            {
                messageToPlayer.DisplayDied();
                Vector2 playerposition = transform.position;
                sendtogoogle.Send(System.DateTime.Now, playerposition, enemy);
                gameObject.SetActive(false);
                pmc.isDead = true;
            }
        }
    }


     

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player exits the collider of an object with the "bridge" tag
        if (other.gameObject.tag == "Bridge")
        {
            isOnBridge = false;
        }
    }


}
