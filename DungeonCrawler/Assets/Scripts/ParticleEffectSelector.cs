using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectSelector : MonoBehaviour {

    //[SerializeField]
    //private GameObject parentTransform;

    [SerializeField]
    private GameObject defaultParticleSystem;
    [SerializeField]
    private GameObject greenParticleSystem;
    [SerializeField]
    private GameObject redParticleSystem;
    [SerializeField]
    private GameObject silverParticleSystem;

    public Substance substance = Substance.none_physical;


    void Start()
    {
        switch (substance)
        {
            case Substance.none_physical:
                Instantiate(defaultParticleSystem, transform);
                break;
            case Substance.green:
                Instantiate(greenParticleSystem, transform);
                break;
            case Substance.red:
                Instantiate(redParticleSystem, transform);
                break;
            case Substance.silver:
                Instantiate(silverParticleSystem, transform);
                break;
        }
    }
}
