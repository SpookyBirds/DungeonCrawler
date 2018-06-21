using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public void StartGame(int StartGameButton)
    {
        SceneManager.LoadScene(StartGameButton);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
   
}
