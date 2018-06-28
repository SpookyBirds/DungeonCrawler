using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
[RequireComponent(typeof(EquipmetHolder))]
public abstract class Controller : InheritanceSimplyfier {


    [SerializeField] [Tooltip("The animator used for this Entity. If not supplied, the script will search the transform and it's children")]
    protected Animator animator;
    [SerializeField]
    protected AnimatorOverrideController animatorOverrideController;
    protected EquipmetHolder equipmetHolder;
    public Entity Entity { get; protected set; }

    /// <summary>
    /// The Entity types 
    /// </summary>
    private int[] enemyTypes;
    public int[] EnemyTypes
    {
        get { return enemyTypes; }
        protected set { enemyTypes = value; }
    }

    protected override void Awake()
    {
        Entity = GetComponentInChildren<Entity>();

        equipmetHolder = GetComponent<EquipmetHolder>();

        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        animator.runtimeAnimatorController = animatorOverrideController;
        animatorOverrideController["DEFAULT_LeftUse"]  = equipmetHolder.LeftHand.animationClip; 
        animatorOverrideController["DEFAULT_RightUse"] = equipmetHolder.RightHand.animationClip;
    }

    /// <summary>
    /// Use the holdable in the left hand. Returns whether it was successfull
    /// </summary>
    public virtual void UseLeft() { }

    /// <summary>
    /// Called after the left action is finished
    /// </summary>
    public virtual void UpdateLeft() { }

    /// <summary>
    /// Use the holdable in the right hand. Returns whether it was successfull
    /// </summary>
    public virtual void UseRight() { }

    /// <summary>
    /// Called after the right action is finished
    /// </summary>
    public virtual void UpdateRight() { }

    /// <summary>
    /// Use this to implement a jump
    /// </summary>
    public virtual void Jump() { }

}
