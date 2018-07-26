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
            cameraMovementController.GamePaused = value;
        }
    }

    public GameObject pauseScreen;
    public GameObject characterScreen;
    public GameObject cameraStop;

    public CameraMovementController cameraMovementController;

    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (GamePaused == false)
            {
                GamePaused = true;
                pauseScreen.SetActive(true);
                characterScreen.SetActive(true);

                
            }
            else if(GamePaused == true)
            {
                GamePaused = false;
                pauseScreen.SetActive(false);
                characterScreen.SetActive(false);

            }
        }
         

        //if (GamePaused == true)
        //{
        //    Time.timeScale = 0;
        //}

        //if (GamePaused == false)
        //{
        //    Time.timeScale = 1;
        //}

    }
}

