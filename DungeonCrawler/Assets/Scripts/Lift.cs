using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lift : MonoBehaviour {

    [SerializeField]
    private int yDestination;

    [SerializeField]
    private float speed;

    private bool isActive;

	void Update () {
        if (isActive == false)
            return;

        transform.Translate(0, -speed, 0);
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag(Global.PlayerTag))
            isActive = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Global.PlayerTag))
            isActive = false;
    }

    //void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag(Global.PlayerTag))
    //    {
    //        if(transform.position.y>yDestination)
    //        {
    //            transform.position -= new Vector3(0, speed, 0);
    //        }
    //    }
    //}
}
