using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(EquipmetHolder))]
public abstract class Controller : InheritanceSimplyfier {

    [EnumFlags] [SerializeField] [Tooltip("All enemy types in hostility with this one")]
    private Entities hostileEntities;
    public Entities HostileEntities
    {
        get { return hostileEntities; }
        protected set { hostileEntities = value; }
    }

    [SerializeField] [Tooltip("The animator used for this Entity. If not supplied, the script will search the transform and it's children")]
    private Animator animator;
    public Animator Animator
    {
        get { return animator; }
        protected set { animator = value; }
    }

    [SerializeField] [Tooltip("The override controller used to dynamically assign the weapon animations")]
    private AnimatorOverrideController animatorOverrideController;
    public AnimatorOverrideController AnimatorOverrideController
    {
        get { return animatorOverrideController; }
        protected set { animatorOverrideController = value; }
    }

    public EquipmetHolder EquipmetHolder { get; protected set; }

    public Rigidbody Rigid { get; protected set; }

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
        Rigid  = GetComponent<Rigidbody>();
        EquipmetHolder = GetComponent<EquipmetHolder>();
        if (Animator == null)
            Animator = GetComponentInChildren<Animator>();
    }

    protected override void Start()
    {
        Animator.runtimeAnimatorController = AnimatorOverrideController;
        AnimatorOverrideController["DEFAULT_LeftUse"]  = EquipmetHolder.LeftHand.animationClip;
        AnimatorOverrideController["DEFAULT_RightUse"] = EquipmetHolder.RightHand.animationClip;
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
