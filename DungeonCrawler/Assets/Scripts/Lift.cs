using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour {

    public bool active;

    [SerializeField]
    private Transform position_1;

    [SerializeField]
    private Transform position_2;

    private Transform destinationPosition;

    [SerializeField]
    private Transform gameLogic;

    [SerializeField]
    private float speed;

    void Start()
    {
        destinationPosition= position_1;
    }

	void Update ()
    {
        if(active)
        {
            MoveLift();
        }
	}

    private void SwitchDestination()
    {
        if (destinationPosition==position_2)
            destinationPosition = position_1;
        else
            destinationPosition = position_2;
    }

    private void MoveLift()
    {
        if (transform.position != destinationPosition.position)
        {
            transform.position = transform.position + (destinationPosition.position - transform.position) * speed * Time.deltaTime;      
        }
        else
        {
            active = false;
            Debug.Log("destination reached");
            SwitchDestination();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Global.PlayerTag))
            other.transform.parent = transform;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Global.PlayerTag))
            other.transform.parent = gameLogic;
    }

}
