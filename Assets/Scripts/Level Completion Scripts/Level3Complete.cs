using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level3Complete : MonoBehaviour
{
    public GameObject level3CompleteUI;
    public GameObject level3Exit;
    public GameObject player;
    public GameObject fireEnemy;
    private SendToGoogle sendtogoogle;
    //private RockEnemy rockEnemyScript;

    void Start()
    {
       sendtogoogle = player.GetComponent<SendToGoogle>();
    }


    // This function is called when the collision into level3 exit trigger at the end of room4
   //function which checks whether win criteria for level 3 is satisfied
   private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
             if (!fireEnemy.activeInHierarchy)
            {   Debug.Log("Level3 Complete");
                sendtogoogle.Send(System.DateTime.Now, transform.position, "WIN");
                level3CompleteUI.SetActive(true);
            }
        }
    }

    public void GoToMainMenu()
    {
       SceneManager.LoadScene("Main Menu");
    }

    public void GoToNextLevel()
    {
       SceneManager.LoadScene("Level 4");
    }



}