using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Holdable : InheritanceSimplyfier {

    public HoldableMode HoldableMode { get; set; }

    [Tooltip("The Collider used to calculate a hit. If it isn't supplied, the script will search it's gameObject as well as children (in ths order)")]
    public BoxCollider influenceCollider;
    public Transform model;
    [Space]
    public Vector3 transformationRotation;
    public Vector3 transformationPosition;
    public AnimationClip animationClip;

    public Vector3 AttackColliderPosition { get { return influenceCollider.transform.position; } }

    /// <summary>
    /// If no attackCollider is supplied, the script will search the it's gameObject as well as children (in ths order) for an attackCollider"
    /// </summary>
    protected override void Awake()
    {
        if (influenceCollider == null)
            influenceCollider = GetComponent<BoxCollider>();
        if (influenceCollider == null)
            influenceCollider = GetComponentInChildren<BoxCollider>();
    }

    /// <summary>
    /// Activates the use action. Returns whether the action is successfull
    /// </summary>
    public abstract bool Use(Controller controller);
}

public enum HoldableMode
{
    SingleClick,
    Hold,
}
