using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(EquipmetHolder))]
public class ControllerPlayer : Controller {

    [SerializeField] private float maxForwardSpeed = 30;
    [SerializeField] private float maxLeftSpeed    = 30;
    [SerializeField] private float maxBackSpeed    = 30;
    [SerializeField] private float maxRightSpeed   = 30;
    [SerializeField] private float maxJumpforce    = 30;
    [SerializeField] private float maxRollSpeed    = 600f;

    private float forwardSpeed;
    private float leftSpeed   ;
    private float backSpeed   ;
    private float rightSpeed  ;
    private float jumpforce   ;
    private float rollSpeed   ;

    [SerializeField] [Tooltip("The override controller used to dynamically assign the weapon animations")]
    private AnimatorOverrideController animatorOverrideController;
    public AnimatorOverrideController AnimatorOverrideController
    {
        get { return animatorOverrideController; }
        protected set { animatorOverrideController = value; }
    }

    /// <summary>
    /// The two Hands of the player. This holds the equiped tools and weapons
    /// </summary>
    public EquipmetHolder EquipmetHolder { get; protected set; }

    /// <summary>
    /// The rigidbody this player uses.
    /// </summary>
    public Rigidbody Rigid { get; protected set; }


    private CameraMovementController cameraMovementController;

    private bool isGrounded;
    public bool IsGrounded
    {
        get { return isGrounded; }
        set
        {
            isGrounded = value;
            Animator.SetBool("GroundContact", IsGrounded);
        }
    }

    private bool isRolling = false;
    private bool lastFrameIsRolling = false;

    protected override void Awake()
    {
        base.Awake();

        cameraMovementController = GetComponentInChildren<CameraMovementController>();

        Rigid = GetComponent<Rigidbody>();
        EquipmetHolder = GetComponent<EquipmetHolder>();

        forwardSpeed = maxForwardSpeed;
        leftSpeed    = maxLeftSpeed;
        backSpeed    = maxBackSpeed;
        rightSpeed   = maxRightSpeed;
        jumpforce    = maxJumpforce;
        rollSpeed    = maxRollSpeed;
    }

    protected override void Start()
    {
        Animator.runtimeAnimatorController = AnimatorOverrideController;

        bool isLeftChainable = EquipmetHolder.LeftHand is ChainableWeapon;
        Animator.SetBool("IsLeftChainable", isLeftChainable);
        AnimatorOverrideController["DEFAULT_Chain_1_LeftUse_short" ] = EquipmetHolder.LeftHand.animationClipShortAttack;
        if (isLeftChainable)
        {
            AnimatorOverrideController["DEFAULT_Chain_2_LeftUse_short"] = EquipmetHolder.LeftHand.animationClipShortAttack;
            AnimatorOverrideController["DEFAULT_Chain_3_LeftUse_short"] = EquipmetHolder.LeftHand.animationClipShortAttack;
        }
        AnimatorOverrideController["DEFAULT_LeftUse_long"  ] = EquipmetHolder.LeftHand.animationClipLongAttack;

        bool isRightChainable = EquipmetHolder.RightHand is ChainableWeapon;
        Animator.SetBool("IsRightChainable", isRightChainable);
        AnimatorOverrideController["DEFAULT_Chain_1_RightUse_short"] = EquipmetHolder.RightHand.animationClipShortAttack;
        if (isRightChainable)
        {
            AnimatorOverrideController["DEFAULT_Chain_2_RightUse_short"] = ((ChainableWeapon)EquipmetHolder.RightHand).chain_2_Attack;
            AnimatorOverrideController["DEFAULT_Chain_3_RightUse_short"] = ((ChainableWeapon)EquipmetHolder.RightHand).chain_3_Attack;
        }
        AnimatorOverrideController["DEFAULT_RightUse_long" ] = EquipmetHolder.RightHand.animationClipLongAttack;
    }

  

    public void Jump()
    {
        Rigid.AddForce(0, jumpforce, 0);
    }

    public void UseLeft(UseType useType, int currentChainLink)
    {
        Debug.Log("useleft " + useType + "  " + currentChainLink);

        if (useType == UseType.shortAttack)
        {
            //Animator.SetBool("UseLeft_short", false);
            EquipmetHolder.LeftHand.UseShort(this);
        }
        else if (useType == UseType.longAttack)
        {
            EquipmetHolder.LeftHand.UseLong(this);
        }
    }

    public void UseRight(UseType useType, int currentChainLink)
    {
        Debug.Log("uiseright "+ useType + "  " + currentChainLink);

        if (useType == UseType.shortAttack)
        {
            //Animator.SetBool("UseRight_short", false);
            EquipmetHolder.RightHand.UseShort(this);
        }
        else if(useType == UseType.longAttack)
        {
            EquipmetHolder.RightHand.UseLong(this);
        }
    }

    public void QuitLeft(UseType useType, int currentChainLink)
    {
        Debug.Log("useType left " + currentChainLink);

        if (useType == UseType.shortAttack)
        {
        }
        else if (useType == UseType.longAttack)
        {
            if(CTRLHub.inst.LeftAttack == false)
            {
                Animator.SetBool("UseLeft_long", false);
                EquipmetHolder.LeftHand.UpdateUse(this, true);
            }
            else
                EquipmetHolder.LeftHand.UpdateUse(this, false);
        }
    }

    public void QuitRight(UseType useType, int currentChainLink)
    {
        Debug.Log("useType right " + currentChainLink);

        if(useType == UseType.shortAttack)
        {
        }
        else if(useType == UseType.longAttack)
        {
            if (CTRLHub.inst.RightFireHold == false)
            {
                Animator.SetBool("UseRight_long", false);
                EquipmetHolder.RightHand.UpdateUse(this, true);
            }
            else
                EquipmetHolder.RightHand.UpdateUse(this, false);
        }
    }

    protected override void Update()
    {
        base.Update();

        isRolling = Animator.GetBool("IsRolling");

        if(lastFrameIsRolling != isRolling)
        {
            if (isRolling)
            {
                forwardSpeed = rollSpeed;
                backSpeed = rollSpeed;
                leftSpeed = rollSpeed;
                rightSpeed = rollSpeed;
                HandleMovement(true);
            }
            else
            {
                forwardSpeed = maxForwardSpeed;
                backSpeed    = maxBackSpeed;
                leftSpeed    = maxLeftSpeed;
                rightSpeed   = maxRightSpeed;
            }
        }

        lastFrameIsRolling = isRolling;
    }

    protected override void FixedUpdate()
    {
        HandleMovement(isRolling == false);
    }

    private void HandleMovement(bool doOrDont)
    {
        if (doOrDont == false)
            return;

        float verticalAxis = CTRLHub.inst.VerticalAxis;
        float horizontalAxis = CTRLHub.inst.HorizontalAxis;

        if (verticalAxis > 0)
        {
            SnapPlayerInCameraDirection();
            Rigid.AddForce(ForwardDirection * forwardSpeed * verticalAxis);
        }
        else if (verticalAxis < 0)
        {
            SnapPlayerInCameraDirection();
            Rigid.AddForce(BackDirection * backSpeed * -verticalAxis);
        }

        if (horizontalAxis > 0)
        {
            SnapPlayerInCameraDirection();
            Rigid.AddForce(RightDirection * leftSpeed * horizontalAxis);
        }
        else if (horizontalAxis < 0)
        {
            SnapPlayerInCameraDirection();
            Rigid.AddForce(LeftDirection * rightSpeed * -horizontalAxis);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IsGrounded = true;
    }


    private void OnTriggerExit(Collider other)
    {
        IsGrounded = false;
    }

    private void SnapPlayerInCameraDirection()
    {
        cameraMovementController.SaveDirection();
        transform.LookAt(transform.position + cameraMovementController.GetStraightCameraDirection());
        cameraMovementController.RestoreDirection();
    }
}
