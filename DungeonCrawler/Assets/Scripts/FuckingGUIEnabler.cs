using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuckingGUIEnabler : MonoBehaviour {

    [SerializeField]
    private GameObject GUI;

    [SerializeField]
    private GameObject breakMenu;

    private bool pausedGame;

    void Awake()
    {
        GUI.SetActive(true);
    }

    private void Update()
    {
        if (CTRLHub.inst.PauseDown)
        {
            if (pausedGame == false)
                PauseGame();
            else
                ContinueGame();    
        }
    }

    private void PauseGame()
    {
        pausedGame = true;
        CursorHandler.inst.activateCourser(true);
        breakMenu.SetActive(true);
    }

    public void ContinueGame()
    {
        pausedGame = false;
        CursorHandler.inst.activateCourser(false);
        breakMenu.SetActive(false);
    }
}
