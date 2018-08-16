﻿using UnityEngine;

public class Test_character : Controller {

    #region fields

    [SerializeField] [Tooltip("Forward speed. Also used for forward-sideways walking (as well as plane sideways walking)")]
    private float forwardSpeed = 20f;

    [SerializeField] [Tooltip("Maximum backwards speed. Also used for backward-sideways walking")]
    private float backwardSpeed = 16f;

    [SerializeField] [Tooltip("Force multiplier applied at the start of a roll")]
    private float rollingStrength = 20f;

    [SerializeField] [Tooltip("Maximum height a player can step over. " +
       "Also the maximum use for slopes (with 'directionCheckingDistance' as delta X and this as delta Y)")]
    private float maxSteppingHeight = 0.5f;

    [SerializeField] [Tooltip("The force representing the upwards portion of a jump")]
    private float jumpForce = 10f;

    [SerializeField] [Tooltip("The amount the character moves in the jump direction while jumping")]
    private float jumpForwardStrength = 20f;

    [SerializeField] [Tooltip("All layers the player gets grounded on")]
    private LayerMask groundingMask;

    [SerializeField] [Tooltip("The distance below the character at which grounding is still detected")]
    private float groundingDistance = 0.15f;

    [SerializeField] [Tooltip("Distance from the character where the slope detecting second rayHit is cast from")]
    private float directionCheckingDistance = 0.25f;

    [SerializeField] [Tooltip("The parameter to smooth the falling. " +
        "Overtime the forward falling speed is lerped against 0 using this value as the speed. " +
        "The higher this value the faster the speed drops")]
    private float dropMovementForceWeakening = 0.5f;

    [SerializeField] [Tooltip("The collider used to check if the player touches the ground " +
        "(the collider is not actually used, only its values. " +
        "Make sure the center stays at the center of the transform, " +
        "and its dimentions are only manipulated via its size, not the transforms scale)")]
    private BoxCollider groundingCollider;


    private bool isGrounded;
    public bool IsGrounded
    {
        get { return isGrounded; }
        set
        {
            isGrounded = value;
            Animator.SetBool("groundContact", IsGrounded);
        }
    }

    public Rigidbody Rigid { get; protected set; }

    /// <summary>
    /// The direction of the gravity, normalized
    /// </summary>
    private Vector3 normalizedGravity;

    /// <summary>
    /// The controller responsible for moving the camera
    /// </summary>
    private CameraMovementController cameraMovementController;

    /// <summary>
    /// Cache of the vertical input axis. Gets updated every frame (see 'Update()')
    /// </summary>
    private float verticalAxis;
    /// <summary>
    /// Cache of the horizontal input axis. Gets updated every frame (see 'Update()')
    /// </summary>
    private float horizontalAxis;

    /// <summary>
    /// Gets updated every frame (see 'HandleRolling()')
    /// </summary>
    private bool isRolling = false;
    /// <summary>
    /// The value of 'isRolling' from the last frame (see 'HandleRolling()')
    /// </summary>
    private bool lastFrameIsRolling = false;

    /// <summary>
    /// Gets updated every frame (see 'mecanim bool setter')
    /// </summary>
    private bool isJumping = false;
    /// <summary>
    /// The value of 'isJumping' from the last frame (see 'HandleJumping()')
    /// </summary>
    private bool lastFrameIsJumping = false;

    /// <summary>
    /// checks if player is running. See 'enter/exit counter'
    /// </summary>
    private int isRunning = 0;

    /// <summary>
    /// The direction the player should move according to the player input. (Updated every frame, see 'HandleInputProcessing()')
    /// </summary>
    private Vector3 inputedMovementDirectionRotated;

    /// <summary>
    /// The direction the player is supposed to get a force to. Already rotated according to the player rotation.
    /// Takes into account falling, rolling, etc. (see 'HandleMovementDirection()')
    /// </summary>
    private Vector3 movementDirection;

    /// <summary>
    /// The point the groundCheckingRay hit the ground. (Set to transform.position if none was found) (see 'HandleGroundCheck()')
    /// </summary>
    private RaycastHit groundCheckHit;

    /// <summary>
    /// The direction the player is getting pushed in while falling. (See 'HandleJump()' and 'dropMovementForceWeakening')
    /// </summary>
    private Vector3 airebornMovementDirection;

    #endregion fields

    protected override void Awake()
    {
        base.Awake();

        Rigid = GetComponent<Rigidbody>();
        cameraMovementController = GetComponentInChildren<CameraMovementController>();
        normalizedGravity = Physics.gravity.normalized;
    }

    protected override void Update ()
    {
        HandleInputProcessing();
        HandleGroundCheck();
        HandleMovementDirection();
        HandleJump();
        HandleRolling();

        Animator.SetBool("jump", CTRLHub.inst.Jump);
        Animator.SetBool("dodgeroll", CTRLHub.inst.Roll);
        if (!Animator.GetBool("weaponSwap"))
        {
            Animator.SetBool("attackRight", CTRLHub.inst.RightAttackDown);
            Animator.SetBool("attackRightHold", CTRLHub.inst.RightAttack);
            Animator.SetBool("attackRightRelease", CTRLHub.inst.RightAttackUp);

            Animator.SetBool("attackLeft", CTRLHub.inst.LeftAttackDown);
            Animator.SetBool("attackLeftHold", CTRLHub.inst.LeftAttack);
            Animator.SetBool("attackLeftRelease", CTRLHub.inst.LeftAttackUp);
        }
    }

    /// <summary>
    /// Handling the caching of the input axis and the calculating of the planned movement
    /// </summary>
    private void HandleInputProcessing()
    {
        // Cache axis input
        if (IsFrozen)
        {
            horizontalAxis = 0;
            verticalAxis = 0;
        }
        else
        {
            horizontalAxis = CTRLHub.inst.HorizontalAxis;
            verticalAxis = CTRLHub.inst.VerticalAxis;
        }

        // Calculate inputed movement (direction and strength)
        inputedMovementDirectionRotated = transform.rotation * new Vector3(horizontalAxis, 0, verticalAxis);
    }

    /// <summary>
    /// Handling raycast to find ground hitting point and updating isGrounded bool via grounding collider
    /// </summary>
    private void HandleGroundCheck()
    {
        if (false == Physics.Raycast(
            transform.position - (normalizedGravity * (groundingDistance / 2)),
            normalizedGravity,
            out groundCheckHit, // private field
            groundingDistance * 1.5f,
            groundingMask))
        {
            groundCheckHit.point = transform.position; // if raycast did't hit, the point is set to transform.position
        }

        Collider[] playerStandingColliders = Physics.OverlapBox(
            groundingCollider.transform.position,
            groundingCollider.size / 2,
            groundingCollider.transform.rotation,
            groundingMask,
            QueryTriggerInteraction.Ignore);

        IsGrounded = playerStandingColliders.Length != 0;

    }

    /// <summary>
    /// Handling directin the player should move (according to physics and other circumstances)
    /// </summary>
    private void HandleMovementDirection()
    {
        /// In the air, all moving is prohibited
        if (IsGrounded == false)
        {
            movementDirection = Vector3.zero;
            return;
        } 

        //Used for BlendTrees in Locomotion(Run,Roll)
        Animator.SetFloat("verticalInput", CTRLHub.inst.VerticalAxis);
        Animator.SetFloat("horizontalInput", CTRLHub.inst.HorizontalAxis);

        //When to run For or Backwards. Forwards includes Sidewards
        Animator.SetBool("runForward", CTRLHub.inst.VerticalAxis > 0 || CTRLHub.inst.HorizontalAxis != 0 && CTRLHub.inst.VerticalAxis == 0);
        Animator.SetBool("runBackward", CTRLHub.inst.VerticalAxis < 0);

        /// On the ground or on a slope, the direction get's calculated via 
        ///  casting an additional ray some distance ('directionCheckingDistance') away 
        ///  (in the planned player direction ('inputedMovementDirectionRotated')) and 
        ///  taking the ray resulting from it and the detected position of the ground below the player ('groundCheckHit')

        // Offset from the player feet
        Vector3 directionCheckingOffset = inputedMovementDirectionRotated * directionCheckingDistance;
        directionCheckingOffset.y += maxSteppingHeight;

        RaycastHit hit;
        if (Physics.Raycast(
            transform.position + directionCheckingOffset,
            normalizedGravity, out hit, maxSteppingHeight * 2f, groundingMask))
        {
            movementDirection = (hit.point - groundCheckHit.point).normalized;
            if (movementDirection.y < 0)
                movementDirection.y = 0; // Deleting force downwards to account for edges and bumps
            return;
        }

        /// If both cases descripted above don't occur, the planned input gets passed. (For example if the player is standing on an edge)

        movementDirection = inputedMovementDirectionRotated;
    }

    /// <summary>
    /// Handling the jump and the airborne movement if the player is airborne
    /// </summary>
    private void HandleJump()
    {
        isJumping = Animator.GetBool("isJumping");

        if (IsGrounded)
        {
            if ((isJumping && lastFrameIsJumping == false) && 
                IsFrozen == false)
            {
                Rigid.AddForce(-normalizedGravity * jumpForce, ForceMode.Impulse);
                airebornMovementDirection = movementDirection;
            }
        }
        else
        {
            Rigid.AddForce(airebornMovementDirection * jumpForwardStrength);
            airebornMovementDirection = Vector3.Lerp(airebornMovementDirection, Vector3.zero, dropMovementForceWeakening * Time.deltaTime);
        }
    }

    /// <summary>
    /// Handling rolling and updating the isRolling parameter
    /// </summary>
    private void HandleRolling()
    {
        isRolling = Animator.GetBool("isRolling");

        if ((isRolling && lastFrameIsRolling == false) &&
            IsFrozen == false)
        {
            ApplyForceInMovementDirection(rollingStrength, ForceMode.Impulse);
        }

        lastFrameIsRolling = isRolling;
    }

    protected override void FixedUpdate()
    {
        // Normal walking movement 
        HandleMovement();
    }

    /// <summary>
    /// Handling movement and snaping player in camera direction
    /// </summary>
    private void HandleMovement()
    {
        isRunning = Animator.GetInteger("isRunning");

        if (isRunning > 0)
        {
            SnapPlayerInCameraDirection();

            if (verticalAxis > 0)
                ApplyForceInMovementDirection(forwardSpeed);
            else
                ApplyForceInMovementDirection(backwardSpeed);
        }
    }

    /// <summary>
    /// Applies AddForce in 'movementDirection' using the given strength and ForceMode,
    /// according to the inputed movement strength ('GetInputMagnitude()')
    /// </summary>
    private void ApplyForceInMovementDirection(float strength, ForceMode forceMode = ForceMode.Force)
    {
        Rigid.AddForce(movementDirection * strength * GetInputMagnitude(), forceMode);
    }

    /// <summary>
    /// Getting the length/strength of the inputted movement direction,
    /// and clamps it between -1 and 1 to prevent higher speed while walking sideways
    /// </summary>
    private float GetInputMagnitude()
    {
        return Mathf.Clamp(inputedMovementDirectionRotated.magnitude, -1, 1);
    }

    /// <summary>
    /// Snaps player in camera direction
    /// </summary>
    private void SnapPlayerInCameraDirection()
    {
        cameraMovementController.SaveDirection();
        transform.LookAt(transform.position + cameraMovementController.GetStraightCameraDirection());
        cameraMovementController.RestoreDirection();
    }
}
