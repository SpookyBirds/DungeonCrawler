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

    [SerializeField]
    private Material mat_emptySubstance;
    [SerializeField]
    private Material mat_silverSubstance;
    [SerializeField]
    private Material mat_greenSubstance;
    [SerializeField]
    private Material mat_redSubstance;

    [SerializeField]
    private MeshRenderer meshRenderer;

    [SerializeField] [Tooltip("In the meshRenderer the number of the substanceMaterial")]
    private int substanceMaterialNumber;

    protected bool isInfused;
    public Vector3 AttackColliderPosition { get { return influenceCollider.transform.position; } }

    /// <summary>
    /// If no attackCollider is supplied, the script will search the it's gameObject as well as children (in ths order) for an attackCollider"
    /// </summary>
    protected override void Awake()
    {
        if (influenceCollider == null)
            influenceCollider = GetComponentInChildren<BoxCollider>();


        //particleSystem_Silver.Stop();
        particleSystem_Silver.gameObject.SetActive(false);
        //particleSystem_Green.Stop();
        particleSystem_Green.gameObject.SetActive(false);
        //particleSystem_Red.Stop();
        particleSystem_Red.gameObject.SetActive(false);
    }

    public void ToggleInfusion(Substance substance, bool toggle)
    {
        isInfused = toggle;

        switch (substance)
        {
            default: return;

            case Substance.silver:
                particleSystem_Silver.gameObject.SetActive(toggle);
                particleSystem_Green.gameObject.SetActive(false);
                particleSystem_Red.gameObject.SetActive(false);

                meshRenderer.materials[substanceMaterialNumber] = mat_silverSubstance;
                break;

            case Substance.green:
                particleSystem_Silver.gameObject.SetActive(false);
                particleSystem_Green.gameObject.SetActive(toggle);
                particleSystem_Red.gameObject.SetActive(false);
                meshRenderer.materials[substanceMaterialNumber] = mat_greenSubstance;

                break;

            case Substance.red:
                particleSystem_Silver.gameObject.SetActive(false);
                particleSystem_Green.gameObject.SetActive(false);
                particleSystem_Red.gameObject.SetActive(toggle);
                meshRenderer.materials[substanceMaterialNumber] = mat_redSubstance;

                break;
        }
    }


    //    switch (substance)
    //    {
    //        default: return;

    //        case Substance.green:
    //            ToggleCurrentSubstance(  particleSystem_Green, toggle);
    //            DeactivateParticleSystem(particleSystem_Red   );
    //            DeactivateParticleSystem(particleSystem_Silver);
    //            break;

    //        case Substance.red:
    //            ToggleCurrentSubstance(  particleSystem_Red, toggle);
    //            DeactivateParticleSystem(particleSystem_Green );
    //            DeactivateParticleSystem(particleSystem_Silver);
    //            break;

    //        case Substance.silver:
    //            ToggleCurrentSubstance(  particleSystem_Silver, toggle);
    //            DeactivateParticleSystem(particleSystem_Green );
    //            DeactivateParticleSystem(particleSystem_Red   );
    //            break;
    //    }
    //}

    //private void ToggleCurrentSubstance(ParticleSystem particleSystem, bool toggle)
    //{
    //    if (toggle)
    //        particleSystem.Play();
    //    else
    //        DeactivateParticleSystem(particleSystem);
    //}

    //private void DeactivateParticleSystem(ParticleSystem particleSystem)
    //{
    //    particleSystem.Stop();
    //    particleSystem.Clear();
    //}
}