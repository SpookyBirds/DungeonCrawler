using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerSupplier : MonoBehaviour {

    public Transform character;
    public CameraController cameraMovementController;

    private void Awake()
    {
        if (character == null)
            Debug.LogWarning("You forgot to put the player in the pointerSupplier on the player camera. Please do so and hit apply.");

        if (cameraMovementController == null)
            Debug.LogWarning("You forgot to put the cameraMovementController in the pointerSupplier on the player camera. Please do so and hit apply.");
    }
}
