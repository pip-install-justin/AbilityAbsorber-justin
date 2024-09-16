using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Complete : MonoBehaviour
{
    public GameObject level1CompleteUI;
    public GameObject torchFire1;
    public GameObject torchFire2;
    public GameObject level1Exit;
    public GameObject player;
    private SendToGoogle sendtogoogle;
    //private RockEnemy rockEnemyScript;
    public GameObject exit_closedDoor;
    public GameObject exit_openDoor;
    void Start()
    {
        sendtogoogle = player.GetComponent<SendToGoogle>();
        // Assumes RockEnemy script is attached to the same GameObject
        // rockEnemyScript = GetComponent<RockEnemy>();

        /* if (rockEnemyScript == null)
         {
             Debug.LogError("No RockEnemy script found on this GameObject.");
         }*/
    }


    // This function is called when the GameObject this script is attached to collides with another GameObject
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
             if (torchFire1.activeInHierarchy && torchFire2.activeInHierarchy)
            {   exit_closedDoor.SetActive(false);
                exit_openDoor.SetActive(true);
                sendtogoogle.Send(System.DateTime.Now, transform.position, "WIN");
                level1CompleteUI.SetActive(true);
            }
        }
    }

    public void GoToMainMenu()
    {
       SceneManager.LoadScene("Main Menu");
    }

    public void GoToNextLevel()
    {
       SceneManager.LoadScene("Level 2");
    }



}