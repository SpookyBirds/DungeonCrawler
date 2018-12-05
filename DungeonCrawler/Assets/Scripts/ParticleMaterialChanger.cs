using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleMaterialChanger : MonoBehaviour {

    private ParticleSystemRenderer PsRenderer;

    private int number;

    [SerializeField]
    private Material[] materials;

    [SerializeField]
    private GameObject entity;

    public Substance substance;

	void Start () {
        PsRenderer = GetComponent<ParticleSystemRenderer>();
        

        switch (substance)
        {
            case Substance.none_physical:
                number = 0;
                break;
            case Substance.green:
                number = 1;
                break;
            case Substance.red:
                number = 2;
                break;
            case Substance.silver:
                number = 3;
                break;
        }
        PsRenderer.material = materials[number];
	}
}
