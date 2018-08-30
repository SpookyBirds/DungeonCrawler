using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeGravity : MonoBehaviour {

    [SerializeField]
    private Animator animator;

    private Rigidbody rigi;

    [SerializeField]
    private float gravityMultiplier = 1;

    [SerializeField]
    private float gravityOverTimeMultiplier = 1;

    private float currentGravityMultiplier;

    void Awake()
    {
        currentGravityMultiplier = gravityMultiplier;
        rigi = GetComponent<Rigidbody>();
    }

    void Update()
    {

        if(animator.GetBool("groundContact"))
        {
            currentGravityMultiplier = gravityMultiplier;
        }
        else if(!animator.GetBool("groundContact"))
        {
            rigi.AddForce(Vector3.down * Time.deltaTime * currentGravityMultiplier);
            currentGravityMultiplier += gravityOverTimeMultiplier;
        }
    }
}
