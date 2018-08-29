using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Holdable : InheritanceSimplyfier
{
    [Tooltip("The Collider used to calculate a hit. If it isn't supplied, the script will search it's gameObject as well as children")]
    public BoxCollider influenceCollider;
    public Transform model;
    [Space]
    [SerializeField]
    private ParticleSystem particleSystem_Silver;
    [SerializeField]
    private ParticleSystem particleSystem_Green;
    [SerializeField]
    private ParticleSystem particleSystem_Red;

    protected bool isInfused;
    public Vector3 AttackColliderPosition { get { return influenceCollider.transform.position; } }

    /// <summary>
    /// If no attackCollider is supplied, the script will search the it's gameObject as well as children (in ths order) for an attackCollider"
    /// </summary>
    protected override void Awake()
    {
        if (influenceCollider == null)
            influenceCollider = GetComponentInChildren<BoxCollider>();
    }

    public void ToggleInfusion(Substance substance, bool toggle)
    {
        isInfused = toggle;

        particleSystem_Silver.Stop();
        particleSystem_Green.Stop();
        particleSystem_Red.Stop();

        if (toggle == false)
            return;

        switch (substance)
        {
            default: return;
            case Substance.green:
                particleSystem_Green.Play();
                break;
            case Substance.red:
                particleSystem_Red.Play();
                break;
            case Substance.silver:
                particleSystem_Silver.Play();
                break;
        }
    }
}