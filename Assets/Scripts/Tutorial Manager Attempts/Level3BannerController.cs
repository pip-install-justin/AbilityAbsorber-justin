using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3BannerController : MonoBehaviour
{
    public GameObject WelcomeBanner;
    public GameObject AbilityChoicePanel;
    
    IEnumerator Start()
    {
        
        // Pause the game.
        Time.timeScale = 0;

        // Display the welcome banner.
        WelcomeBanner.SetActive(true);

        // Wait for 2 seconds.
        yield return new WaitForSecondsRealtime(2f);

        // Hide the welcome banner.
        WelcomeBanner.SetActive(false);
        
        // Resume the game.
        Time.timeScale = 1;

        // If the game has been restarted, show the AbilityChoicePanel immediately
        if (PauseMenuController.gameRestarted)
        {
            DisplayAbilityChoicePanel();
            
            // Reset the gameRestarted flag to false so that this only applies once
            PauseMenuController.gameRestarted = false;
        }
        else
        {
            // If the game hasn't been restarted, wait for the overview to complete
            CameraMovement.OnOverviewComplete += DisplayAbilityChoicePanel;
        }

        
    }

     void DisplayAbilityChoicePanel() {
        // Unsubscribe from event
        CameraMovement.OnOverviewComplete -= DisplayAbilityChoicePanel;

        // Display the AbilityChoicePanel.
        AbilityChoicePanel.SetActive(true);

         // Resume the game.
        Time.timeScale = 1;
    }

/*

    void Update()
    {  
        if (Input.GetKeyDown(KeyCode.Q))
        {
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
            if (EnemiesBanner.activeInHierarchy)
            {
                EnemiesBanner.SetActive(false);
                Time.timeScale = 1;
            }
        
            if (AbilityBanner.activeInHierarchy)
            {
                AbilityBanner.SetActive(false);
                Time.timeScale = 1;
            }

            if (RiverBanner.activeInHierarchy)
            {
                RiverBanner.SetActive(false);
                Time.timeScale = 1;
            }

            if (BossEnemyBanner.activeInHierarchy)
            {
                BossEnemyBanner.SetActive(false);
                Time.timeScale = 1;
            }
        }

        // Check if fire ability is unlocked and the banner hasn't been displayed yet.
        if (abilityManager.unlockedAbilities["fire"] && !fireAbilityUnlocked && Input.GetKeyDown(KeyCode.Alpha1))
        {
            // Display the ability banner.
            AbilityBanner.SetActive(true);
            Time.timeScale = 0;
            // Mark the fire ability as unlocked
            fireAbilityUnlocked = true;
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
*/
   
}
