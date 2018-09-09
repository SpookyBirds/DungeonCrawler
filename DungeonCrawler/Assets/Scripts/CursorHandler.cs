using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorHandler : MonoBehaviour {

    public static CursorHandler inst;

    [SerializeField]
    private GameObject cameraController;

    [SerializeField]
    private bool startWithCursorActiv;

    void Awake()
    {
        inst = this;
        activateCourser(startWithCursorActiv);
    }

    public void activateCourser(bool activ)
    {
        if(activ)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            cameraController.GetComponent<CameraController>().enabled = false;
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            cameraController.GetComponent<CameraController>().enabled = true;
        }
    }
}
