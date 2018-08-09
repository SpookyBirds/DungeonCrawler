using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGUIEnabler : MonoBehaviour {

    GameObject playerGUI;
    void Start()
    {
        playerGUI = GameObject.Find("PlayerGUI");
        playerGUI.SetActive(true);
    }
	

}
