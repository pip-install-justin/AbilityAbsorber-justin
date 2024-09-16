using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{   public GameObject levelPanel;
    // Function called when Start button is pressed
    public void StartGame()
    {
        SceneManager.LoadScene("Preface");
    }

    // Function called when Options button is pressed
    public void Levels()
    {
        levelPanel.SetActive(true);
    }
    public void Level1()
    {
        SceneManager.LoadScene("Level 1");
    }
    public void Level2()
    {
        SceneManager.LoadScene("Level 2");
    }
    public void Level3()
    {
        SceneManager.LoadScene("Level 3");
    }
    public void Level4()
    {
        SceneManager.LoadScene("Level 4");
    }

    // Function called when Quit button is pressed
    public void QuitGame()
    {
        //If we are running in a standalone build of the game
	#if UNITY_STANDALONE
		//Quit the application
		Application.Quit();
	#endif

		//If we are running in the editor
	#if UNITY_EDITOR
		//Stop playing the scene
		UnityEditor.EditorApplication.isPlaying = false;
	#endif
    }
}
