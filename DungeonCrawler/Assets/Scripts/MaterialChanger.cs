using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour {

    private MeshRenderer meshRenderer;

    private Substance substance = Substance.none_physical;

    [SerializeField] [Tooltip("In the meshRenderer the number of the substanceMaterial")]
    private int substanceMaterialNumber;

    [SerializeField][Tooltip("0 = physical; 1 = green; 2 = red; 3 = silver")]
    private Material[] myMaterials;

    private Material[] meshRendererMaterials;

    [SerializeField]
    private bool lookInParentForEntity;

	void Start () {

        if(lookInParentForEntity)
            substance = gameObject.GetComponentInParent<Entity>().InfusedSubstance;
        else
            substance = GetComponent<Entity>().InfusedSubstance;


        meshRenderer = GetComponent<MeshRenderer>();
        meshRendererMaterials = meshRenderer.materials;

        switch(substance)
        {
            case Substance.none_physical:
                meshRendererMaterials[substanceMaterialNumber] = myMaterials[0];
                break;
            case Substance.green:
                meshRendererMaterials[substanceMaterialNumber] = myMaterials[1];
                break;
            case Substance.red:
                meshRendererMaterials[substanceMaterialNumber] = myMaterials[2];
                break;
            case Substance.silver:
                meshRendererMaterials[substanceMaterialNumber] = myMaterials[3];
                break;
        }

        meshRenderer.materials = meshRendererMaterials;
	}
}
