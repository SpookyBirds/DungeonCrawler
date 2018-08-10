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

    private bool inventoryUi = false;
    private bool InventoryUi
    {
        get { return inventoryUi; }
        set
        {
            inventoryUi = value;
            cameraMovementController.InventoryUi = value;
        }
    }

    public GameObject pauseScreen;
    public GameObject characterScreen;
    public GameObject cameraStop;

    public CameraMovementController cameraMovementController;

    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GamePaused == false)
            {
                GamePaused = true;
            }
            else if(GamePaused == true)
            {
                GamePaused = false;
            }
        }
         
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if(InventoryUi == false)
            {
                InventoryUi = true;
                pauseScreen.SetActive(true);
                characterScreen.SetActive(true);
            }
            else
            {
                InventoryUi = false;
                pauseScreen.SetActive(false);
                characterScreen.SetActive(false);
            }
        }
        

    }
}

