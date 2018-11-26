using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOnCollision : MonoBehaviour {

    [SerializeField]
    private GameObject objectToCreate;

    void OnCollisionEnter()
    {
        objectToCreate.SetActive(true);
    }
}
