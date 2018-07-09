using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(EquipmetHolder))]
public class ControllerPlayer : Controller {

    [SerializeField] private float forwardSpeed = 0.1f;
    [SerializeField] private float leftSpeed = 0.1f;
    [SerializeField] private float backSpeed = 0.1f;
    [SerializeField] private float rightSpeed = 0.1f;
    [SerializeField] private float jumpforce = 1f;

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

    protected override void Awake()
    {
        base.Awake();

        cameraMovementController = GetComponentInChildren<CameraMovementController>();

        Rigid = GetComponent<Rigidbody>();
        EquipmetHolder = GetComponent<EquipmetHolder>();
    }

    protected override void Start()
    {
        Animator.runtimeAnimatorController = AnimatorOverrideController;
        AnimatorOverrideController["DEFAULT_LeftUse_short"]  = EquipmetHolder.LeftHand.animationClipShortAttack;
        AnimatorOverrideController["DEFAULT_RightUse_short"] = EquipmetHolder.RightHand.animationClipShortAttack;
        AnimatorOverrideController["DEFAULT_LeftUse_long"]   = EquipmetHolder.LeftHand.animationClipLongAttack;
        AnimatorOverrideController["DEFAULT_RightUse_long"]  = EquipmetHolder.RightHand.animationClipLongAttack;
    }

    public void Jump()
    {
        Rigid.AddForce(0, jumpforce, 0);
    }

    public void UseLeft(UseType useType)
    {
        Debug.Log("useleft " + useType);

        if (useType == UseType.shortAttack)
        {
            Animator.SetBool("UseLeft_short", false);
            EquipmetHolder.LeftHand.UseShort(this);
        }
        else if (useType == UseType.longAttack)
        {
            EquipmetHolder.LeftHand.UseLong(this);
        }
    }

    public void UseRight(UseType useType)
    {
        Debug.Log("uiseright "+ useType);

        if (useType == UseType.shortAttack)
        {
            Animator.SetBool("UseRight_short", false);
            EquipmetHolder.RightHand.UseShort(this);
        }
        else if(useType == UseType.longAttack)
        {
            EquipmetHolder.RightHand.UseLong(this);
        }
    }

    public void QuitLeft(UseType useType)
    {
        if(useType == UseType.shortAttack)
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

    public void QuitRight(UseType useType)
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

    /* Old attack methods
    public void UseLeft()
    {
        if (EquipmetHolder.LeftHand.HoldableMode == HoldableMode.SingleClick)
        {
            Animator.SetBool("UseLeft", false);
            EquipmetHolder.LeftHand.UseShort(this);
        }
        else if (EquipmetHolder.LeftHand.HoldableMode == HoldableMode.Hold)
        {
            EquipmetHolder.LeftHand.UseShort(this);
        }
    }

    public void UseRight()
    {
        if (EquipmetHolder.RightHand.HoldableMode == HoldableMode.SingleClick)
        {
            Animator.SetBool("UseRight", false);
            EquipmetHolder.RightHand.UseShort(this);
        }
        else if (EquipmetHolder.RightHand.HoldableMode == HoldableMode.Hold)
        {
            EquipmetHolder.RightHand.UseShort(this);
        }
    }

    public void QuitLeft()
    {
        if (EquipmetHolder.LeftHand.HoldableMode == HoldableMode.Hold)
        {
            if (CTRLHub.inst.LeftAttack == false)
            {
                Animator.SetBool("UseLeft", false);
                (EquipmetHolder.LeftHand as Shield).UpdateUse(this, true);
            }
            else
                (EquipmetHolder.LeftHand as Shield).UpdateUse(this, false);
        }
    }

    public void QuitRight()
    {
        if (EquipmetHolder.RightHand.HoldableMode == HoldableMode.Hold)
        {
            if (CTRLHub.inst.RightAttack == false)
            {
                Animator.SetBool("UseRight", false);
                (EquipmetHolder.RightHand as Shield).UpdateUse(this, true);
            }
            else
                (EquipmetHolder.RightHand as Shield).UpdateUse(this, false);
        }
    }
    */

    protected override void FixedUpdate()
    {
        float verticalAxis   = CTRLHub.inst.VerticalAxis;
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
