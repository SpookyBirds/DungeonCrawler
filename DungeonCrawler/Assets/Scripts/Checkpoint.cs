using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    [SerializeField]
    private GameObject checkpoint;

   void OnTriggerEnter(Collider other)
    {
        checkpoint.SetActive(true);
    }
}
