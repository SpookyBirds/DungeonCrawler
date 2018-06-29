using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(EquipmetHolder))]
public abstract class Controller : InheritanceSimplyfier {

    [EnumFlags] [SerializeField] [Tooltip("All enemy types in hostility with this one")]
    protected Entities hostileEntities;
    [SerializeField] [Tooltip("The animator used for this Entity. If not supplied, the script will search the transform and it's children")]
    protected Animator animator;
    [SerializeField] [Tooltip("The override controller used to dynamically assign the weapon animations")]
    protected AnimatorOverrideController animatorOverrideController;

    protected EquipmetHolder equipmetHolder;
    protected Rigidbody rigid;

    /// <summary>
    /// The entity controlled by this controller
    /// </summary>
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

    /// Entity facing directions
    public Vector3 ForwardDirection { get { return transform.forward; } }
    public Vector3 LeftDirection { get { return -transform.right; } }
    public Vector3 BackDirection { get { return -transform.forward; } }
    public Vector3 RightDirection { get { return transform.right; } }

    protected override void Awake()
    {
        EnemyTypes = Global.GetSelectedEntries(hostileEntities);
        Entity = GetComponentInChildren<Entity>();
        rigid  = GetComponent<Rigidbody>();
        equipmetHolder = GetComponent<EquipmetHolder>();
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    protected override void Start()
    {
        animator.runtimeAnimatorController = animatorOverrideController;
        animatorOverrideController["DEFAULT_LeftUse"] = equipmetHolder.LeftHand.animationClip;
        animatorOverrideController["DEFAULT_RightUse"] = equipmetHolder.RightHand.animationClip;
    }

    /// <summary>
    /// Use the holdable in the left hand. Returns whether it was successfull
    /// </summary>
    public virtual void UseLeft() { }

    /// <summary>
    /// Called after the left action is finished
    /// </summary>
    public virtual void QuitLeft() { }

    /// <summary>
    /// Use the holdable in the right hand. Returns whether it was successfull
    /// </summary>
    public virtual void UseRight() { }

    /// <summary>
    /// Called after the right action is finished
    /// </summary>
    public virtual void QuitRight() { }

    /// <summary>
    /// Use this to implement a jump
    /// </summary>
    public virtual void Jump() { }

}
