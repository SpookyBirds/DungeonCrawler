using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManager : MonoBehaviour {

    private bool gamePaused = false;
    private bool GamePaused
    {
        get { return gamePaused; }
        set
        {
            gamePaused = value;
            cameraMovementController.gamePaused = GamePaused;
        }
    }

    public GameObject PauseScreen;
    public GameObject CameraStop;

    public CameraMovementController cameraMovementController;

    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused == false)
            {
                GamePaused = true;
                PauseScreen.SetActive(true);

                
            }
            else if(GamePaused == true)
            {
                GamePaused = false;
                PauseScreen.SetActive(false);

            }
        }
         

        if (GamePaused == true)
        {
            Time.timeScale = 0;
        }

        if (GamePaused == false)
        {
            Time.timeScale = 1;
        }

    }
}

