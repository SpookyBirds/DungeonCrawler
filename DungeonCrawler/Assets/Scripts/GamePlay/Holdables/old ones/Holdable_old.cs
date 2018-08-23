using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Holdable_old : InheritanceSimplyfier {

    [Tooltip("The Collider used to calculate a hit. If it isn't supplied, the script will search it's gameObject as well as children (in ths order)")]
    public BoxCollider influenceCollider;
    public Transform model;
    [Space]
    public Vector3 transformationRotation;
    public Vector3 transformationPosition;
    public AnimationClip animationClipLongAttack;
    public AnimationClip animationClipShortAttack;

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
    /// Activates the short use action. Returns whether the action is successfull
    /// </summary>
    public abstract bool UseShort(Controller controller);

    /// <summary>
    /// Activates the long use action. Returns whether the action is successfull
    /// </summary>
    public abstract bool UseLong(Controller controller);

    /// <summary>
    /// Called one per frame while the holdable is used.
    /// </summary>
    /// <param name="quit">True if the use is ended</param>
    public virtual void UpdateUse(Controller controller, bool quit) { }
}