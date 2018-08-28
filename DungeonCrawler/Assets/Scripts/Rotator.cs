using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    [SerializeField]
    private int xRotationAmount;

    [SerializeField]
    private int yRotationAmount;

    [SerializeField]
    private int zRotationAmount;

   
    void Update ()
    {
        transform.Rotate(xRotationAmount, yRotationAmount * Time.deltaTime, zRotationAmount);
    }
}
