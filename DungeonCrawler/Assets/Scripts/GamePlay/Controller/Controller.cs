using UnityEngine;

[RequireComponent(typeof(Entity))]
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

    private bool isFrozen;
    public bool IsFrozen
    {
        get { return isFrozen; }
        protected set { isFrozen = value; }
    }

    private int blindingCounter;
    public bool IsBlind { get { return blindingCounter >= 0; } }
    public int BlindingCounter
    {
        get { return blindingCounter; }
        set
        {
            blindingCounter = value;

            if (blindingCounter == 0)
                RemoveBlindEffect();
            else if (blindingCounter > 0)
                ApplyBlindEffect();
            else
                BlindingCounter = 0;
        }
    }

    /// Entity facing directions
    public Vector3 ForwardDirection { get { return  transform.forward; } }
    public Vector3 LeftDirection    { get { return -transform.right;   } }
    public Vector3 BackDirection    { get { return -transform.forward; } }
    public Vector3 RightDirection   { get { return  transform.right;   } }


    protected override void Awake()
    {
        EnemyTypes = Global.GetSelectedEntries(hostileEntities);
        Entity = GetComponentInChildren<Entity>();
        if (Animator == null)
            Animator = GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// Halt player movement and inputs
    /// </summary>
    public virtual void Freeze(float duration)
    {
        Animator.speed = 0.000000000000000001f;

        IsFrozen = true;
        Invoke("UnFreeze", duration);
    }

    /// <summary>
    /// Removes freezed and resumes player movement and inputs
    /// </summary>
    public virtual void UnFreeze()
    {
        Animator.speed = 1f;

        IsFrozen = false;
    }

    /// <summary>
    /// Used if entity is getting blinded. Increases an internal counter, which will apply the blinding effect if neccessary
    /// </summary>
    public void Blind()
    {
        BlindingCounter++;
    }

    /// <summary>
    /// Used if entity stops getting blinded. Decreases an internal counter, which will remove the blinding effect if neccessary
    /// </summary>
    public void UnBlind()
    {
        BlindingCounter--;
    }

    /// <summary>
    /// Applies the blinding effect. Don't call outside of the blindingCounter setter.
    /// </summary>
    protected virtual void ApplyBlindEffect() { }

    /// <summary>
    /// Removes the blinding effect. Don't call outside of the blindingCounter setter.
    /// </summary>
    protected virtual void RemoveBlindEffect() { }
}
