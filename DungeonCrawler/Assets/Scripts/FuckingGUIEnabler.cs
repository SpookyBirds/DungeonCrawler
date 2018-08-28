using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuckingGUIEnabler : MonoBehaviour {

    [SerializeField]
    private GameObject GUI;

    void Awake()
    {
        GUI.SetActive(true);
    }
}
