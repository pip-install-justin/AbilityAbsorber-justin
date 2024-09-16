using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1BannerController : MonoBehaviour
{
    public GameObject WelcomeBanner;
    public GameObject MovementBanner;
    public GameObject InteractionBanner;
    public GameObject AbilityBanner;
    public GameObject EnemiesBanner;
    public GameObject BossEnemyBanner;
    public GameObject RiverBanner;
    public GameObject PuzzleBanner;
    //public GameObject HealthBanner;
    public AbilityManager abilityManager; // Assuming this is set in the inspector.
   
    private bool fireAbilityUnlocked = false;

    IEnumerator Start()
    {
        abilityManager = GetComponent<AbilityManager>();
       
         // Get the currently active scene
        Scene currentScene = SceneManager.GetActiveScene();
        // Pause the game.
        Time.timeScale = 0;

        // Display the welcome banner.
        WelcomeBanner.SetActive(true);

        // Wait for 2 seconds.
        yield return new WaitForSecondsRealtime(2f);

        // Hide the welcome banner.
        WelcomeBanner.SetActive(false);
        Time.timeScale = 1;

        // Check if the name of the current scene is 'level 1' as in tutorial, so so , diaply movement banner
        if(currentScene.name == "Level 1")
        {
            // Display the movement banner.
            // Subscribe to overview movement complete event
            CameraMovement.OnOverviewComplete += DisplayMovementBanner;
        }
        
    }

    void DisplayMovementBanner() {
        // Unsubscribe from event
        CameraMovement.OnOverviewComplete -= DisplayMovementBanner;

        // Display the movement banner.
        MovementBanner.SetActive(true);
    }

    void Update()
    {  
        if (Input.GetKeyDown(KeyCode.Q))
        {
            print(Time.timeScale);
            if (MovementBanner.activeInHierarchy)
            {
                MovementBanner.SetActive(false);
                Time.timeScale = 1;
            }

            if (InteractionBanner.activeInHierarchy)
            {
                InteractionBanner.SetActive(false);
                Time.timeScale = 1;
            }

            
            /*if (HealthBanner.activeInHierarchy)
            {
                HealthBanner.SetActive(false);
                Time.timeScale = 1;
            }
            */

            if (EnemiesBanner.activeInHierarchy)
            {
                EnemiesBanner.SetActive(false);
                Time.timeScale = 1;
                /*CameraMovement cameraMovement = Camera.main.GetComponent<CameraMovement>();

                if (cameraMovement != null)
                {
                    cameraMovement.targetObject = GameObject.FindGameObjectWithTag("BatEnemy");
                    cameraMovement.focusOnObject = true;
                    Debug.Log("Bats set to be focused upon");
                }*/
            }
        
            if (AbilityBanner.activeInHierarchy)
            {
                AbilityBanner.SetActive(false);
                Time.timeScale = 1;
                // Focus camera on the 'woodblock' GameObject
                
                      
            }

            if (RiverBanner.activeInHierarchy)
            {
                RiverBanner.SetActive(false);
                Time.timeScale = 1;
               CameraMovement cameraMovement = Camera.main.GetComponent<CameraMovement>();
                if (cameraMovement != null)
                {
                    cameraMovement.targetObject = GameObject.FindGameObjectWithTag("FireWall");
                    cameraMovement.focusOnObject = true;
                    Debug.Log("Firewall set to be focused upon");
                }
            }

            if (BossEnemyBanner.activeInHierarchy)
            {
                BossEnemyBanner.SetActive(false);
                Time.timeScale = 1;
                CameraMovement cameraMovement = Camera.main.GetComponent<CameraMovement>();
                if (cameraMovement != null)
                {
                    cameraMovement.targetObject = GameObject.FindGameObjectWithTag("BatEnemy");
                    cameraMovement.focusOnObject = true;
                    Debug.Log("Boss Bat set to be focused upon");
                }
            }

            if (PuzzleBanner.activeInHierarchy)
            {
                PuzzleBanner.SetActive(false);
                Time.timeScale = 1;
                
            }
        }

        // Check if fire ability is unlocked and the banner hasn't been displayed yet.
        if (abilityManager.abilityInventory.Contains("fire") && !fireAbilityUnlocked && Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Display the ability banner.
            AbilityBanner.SetActive(true);
            
            // Mark the fire ability as unlocked
            fireAbilityUnlocked = true;//already shown?Yes - so that the banner is not shown each time it is equipped
            Time.timeScale = 0;
        }
            
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Room Switch1")
        {
            yield return new WaitForSecondsRealtime(0.0f);
            Time.timeScale = 0;
            InteractionBanner.SetActive(true);
        }

        if (other.gameObject.name == "Room Switch2")
        {
            yield return new WaitForSecondsRealtime(0.0f);
            Time.timeScale = 0;
            EnemiesBanner.SetActive(true);
        }
    }

   
}
