using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloth_Test : MonoBehaviour
{

    private Animator anim;
    public float horizontalInput;
    public float verticalInput;
   
   

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }


    void Update()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
    }

}