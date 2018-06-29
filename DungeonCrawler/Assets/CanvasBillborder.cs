using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBillborder : MonoBehaviour {

    private void OnGUI()
    {

    }

    private void LateUpdate()
    {
        transform.LookAt(Camera.main.transform.position);

    }
}
