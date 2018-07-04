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
        AnimatorOverrideController["DEFAULT_LeftUse"] = EquipmetHolder.LeftHand.animationClip;
        AnimatorOverrideController["DEFAULT_RightUse"] = EquipmetHolder.RightHand.animationClip;
    }


    public void Jump()
    {
        Rigid.AddForce(0, jumpforce, 0);
    }

    public void UseLeft()
    {
        if (EquipmetHolder.LeftHand.HoldableMode == HoldableMode.SingleClick)
        {
            Animator.SetBool("UseLeft", false);
            EquipmetHolder.LeftHand.Use(this);
        }
        else if (EquipmetHolder.LeftHand.HoldableMode == HoldableMode.Hold)
        {
            EquipmetHolder.LeftHand.Use(this);
        }
    }

    public void UseRight()
    {
        if (EquipmetHolder.RightHand.HoldableMode == HoldableMode.SingleClick)
        {
            Animator.SetBool("UseRight", false);
            EquipmetHolder.RightHand.Use(this);
        }
        else if (EquipmetHolder.RightHand.HoldableMode == HoldableMode.Hold)
        {
            EquipmetHolder.RightHand.Use(this);
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

    protected override void FixedUpdate()
    {
        if (CTRLHub.inst.Forward)
        {
            SnapPlayerInCameraDirection();
            Rigid.AddForce(ForwardDirection * forwardSpeed);
        }

        if (CTRLHub.inst.Left)
        {
            SnapPlayerInCameraDirection();
            Rigid.AddForce(LeftDirection* leftSpeed);
        }
        if (CTRLHub.inst.Back)
        {
            SnapPlayerInCameraDirection();
            Rigid.AddForce(BackDirection* backSpeed);
        }
        if (CTRLHub.inst.Right)
        {
            SnapPlayerInCameraDirection();
            Rigid.AddForce(RightDirection* rightSpeed);
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
        transform.LookAt(transform.position + cameraMovementController.GetCameraDirection());
        cameraMovementController.RestoreDirection();
    }
}
