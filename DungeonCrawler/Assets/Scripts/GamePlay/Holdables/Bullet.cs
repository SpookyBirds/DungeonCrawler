using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public Substance substance = Substance.none_physical;

    [SerializeField]
    private GameObject explosionHolder;


    void OnCollisionEnter(Collision other)
    {
        explosionHolder.GetComponent<ParticleEffectSelector>().substance = substance;
        explosionHolder.SetActive(true);
        explosionHolder.transform.parent = Global.inst.Drops.transform;

        Destroy(gameObject);
    }
}
