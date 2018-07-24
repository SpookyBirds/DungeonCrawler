using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(EquipmetHolder))]
public class ControllerPlayer : Controller {

    [SerializeField] private float maxForwardSpeed = 20f;
    [SerializeField] private float maxBackSpeed    = 20f;
    [SerializeField] private float maxJumpforce    = 10f;
    [SerializeField] private float jumpForwardStrength = 5f;
    [SerializeField] private float maxRollStrength = 20f;
    [Space]
    [SerializeField] private LayerMask groundingMask;
    [SerializeField] private float groundingDistance = 0.15f;
    [SerializeField] private float directionCheckingDistance = 0.25f;
    [SerializeField] private float maxSteppingHeight = 0.5f;

    private float forwardSpeed;
    private float backSpeed;
    private float jumpforce;
    private float rollStrength;

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

    [SerializeField] private BoxCollider groundingCollider;

    private Vector3 normalizedGravity;

    private CameraMovementController cameraMovementController;

    private float verticalAxis;
    private float horizontalAxis;

    private bool isRolling = false;
    private bool lastFrameIsRolling = false;

    private Vector3 inputedMovementDirectionRotated;
    private Vector3 movementDirection;
    private Vector3 previousPosition;
    private RaycastHit groundCheckHit;

    private Vector3 actualMovementDirection;

    protected override void Awake()
    {
        base.Awake();

        cameraMovementController = GetComponentInChildren<CameraMovementController>();

        Rigid = GetComponent<Rigidbody>();
        EquipmetHolder = GetComponent<EquipmetHolder>();

        forwardSpeed = maxForwardSpeed;
        backSpeed    = maxBackSpeed;
        jumpforce    = maxJumpforce;
        rollStrength = maxRollStrength;

        normalizedGravity = Physics.gravity.normalized;
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

    public void UseLeft(UseType useType, int currentChainLink)
    {
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
        HandleInputProcessing();
        HandleGroundCheck();
        HandleMovementDirection();
        HandleJump();
        HandleRolling();
    }

    private void HandleInputProcessing()
    {
        // Cache axis input
        horizontalAxis = CTRLHub.inst.HorizontalAxis;
        verticalAxis = CTRLHub.inst.VerticalAxis;

        // Calculate inputed movement (direction and strength)
        inputedMovementDirectionRotated = transform.rotation * new Vector3(horizontalAxis, 0, verticalAxis);
    }

    private void HandleGroundCheck()
    {
        if(false == Physics.Raycast(
            transform.position - (normalizedGravity * (groundingDistance / 2)),
            normalizedGravity,
            out groundCheckHit,
            groundingDistance * 1.5f,
            groundingMask))
        {
            groundCheckHit.point = transform.position;
        }

        Collider[] playerStandingColliders =  Physics.OverlapBox(
            groundingCollider.transform.position,
            groundingCollider.size / 2,
            groundingCollider.transform.rotation, 
            groundingMask, 
            QueryTriggerInteraction.Ignore);

        IsGrounded = playerStandingColliders.Length != 0;
        
    }

    private RaycastHit hit;

    private void HandleMovementDirection()
    {
        // In the air, all moving is prohibited
        if(IsGrounded == false)
        {
            movementDirection = Vector3.zero;
            previousPosition = transform.position;
            return;
        }

        Vector3 directionCheckingOffset =
            inputedMovementDirectionRotated * directionCheckingDistance + 
            new Vector3(0, maxSteppingHeight, 0);

        //RaycastHit hit;
        if (Physics.Raycast(
            transform.position + directionCheckingOffset, 
            normalizedGravity, out hit, maxSteppingHeight * 2f, groundingMask))
        {
            movementDirection = (hit.point - groundCheckHit.point).normalized;
            if (movementDirection.y < 0) movementDirection.y = 0;
            previousPosition = transform.position;
            return;
        }

        movementDirection = inputedMovementDirectionRotated;
        previousPosition = transform.position;
    }

    private void HandleJump()
    {
        if (IsGrounded)
        {
            if (CTRLHub.inst.JumpDown)
            {
                Rigid.AddForce(-normalizedGravity * jumpforce, ForceMode.Impulse);
                actualMovementDirection = movementDirection;
            }
        }
        else
        {
            Rigid.AddForce(actualMovementDirection * jumpForwardStrength);
        }
    }

    private void HandleRolling()
    {
        isRolling = Animator.GetBool("IsRolling");

        if (isRolling == true &&
            lastFrameIsRolling == false  )
        {
            ApplyForceInMovementDirection(rollStrength, ForceMode.Impulse);
        }

        lastFrameIsRolling = isRolling;
    }

    protected override void FixedUpdate()
    {
        HandleMovement(isRolling == false);
    }

    private void HandleMovement(bool doOrDont)  // Potential fix: making the collider actually bob
    {
        if (doOrDont == false)
            return;

        if (verticalAxis > 0)
        {
            SnapPlayerInCameraDirection();
            ApplyForceInMovementDirection(forwardSpeed);
        }
        else
        {
            SnapPlayerInCameraDirection();
            ApplyForceInMovementDirection(backSpeed);
        }
    }

    private void ApplyForceInMovementDirection(float strength, ForceMode forceMode = ForceMode.Force)
    {
        Rigid.AddForce(movementDirection * strength * GetInputMagnitude(), forceMode);
    }

    private float GetInputMagnitude()
    {
        return Mathf.Clamp(inputedMovementDirectionRotated.magnitude, -1, 1);
    }

    private void SnapPlayerInCameraDirection()
    {
        cameraMovementController.SaveDirection();
        transform.LookAt(transform.position + cameraMovementController.GetStraightCameraDirection());
        cameraMovementController.RestoreDirection();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(hit.point, 0.05f);

        Gizmos.DrawRay(transform.position, movementDirection);
    }
}
