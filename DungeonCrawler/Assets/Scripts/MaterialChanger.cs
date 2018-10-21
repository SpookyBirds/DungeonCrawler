using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour {

    private MeshRenderer meshRenderer;

    private Substance substance = Substance.none_physical;

    [SerializeField] [Tooltip("In the meshRenderer the number of the substanceMaterial")]
    private int substanceMaterialNumber;

    [SerializeField]
    private Material[] myMaterials;

    private Material[] meshRendererMaterials;

	void Start () {

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
