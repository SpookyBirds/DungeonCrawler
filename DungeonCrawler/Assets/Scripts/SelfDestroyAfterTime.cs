using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroyAfterTime : MonoBehaviour {

    [SerializeField]
    private float maxLifetime;

    void Update()
    {
        maxLifetime -= Time.deltaTime;

        if (maxLifetime <= 0)
            Destroy(gameObject);
    }
}
