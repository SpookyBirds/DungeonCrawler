using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour {

    private MeshRenderer meshRenderer;

    

    [SerializeField]
    private Material[] myMaterials;

	void Start () {
        meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.materials = myMaterials;
	}
}
