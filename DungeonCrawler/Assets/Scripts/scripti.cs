using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scripti : MonoBehaviour {

    private ParticleSystemRenderer ps;

    private int number;

    [SerializeField]
    private Material[] materials;

    [SerializeField]
    private GameObject entity;

    public Substance substance;

	void Start () {
        ps = GetComponent<ParticleSystemRenderer>();
        

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
        ps.material = materials[number];
	}
}
