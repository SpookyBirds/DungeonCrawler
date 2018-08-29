using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Holdable : InheritanceSimplyfier
{
    [Tooltip("The Collider used to calculate a hit. If it isn't supplied, the script will search it's gameObject as well as children")]
    public BoxCollider influenceCollider;
    public Transform model;

    public Vector3 AttackColliderPosition { get { return influenceCollider.transform.position; } }

    protected bool isInfused;

    /// <summary>
    /// If no attackCollider is supplied, the script will search the it's gameObject as well as children (in ths order) for an attackCollider"
    /// </summary>
    protected override void Awake()
    {
        if (influenceCollider == null)
            influenceCollider = GetComponentInChildren<BoxCollider>();
    }

    public void ToggleInfusion(bool toggle)
    {
        isInfused = toggle;
    }
}