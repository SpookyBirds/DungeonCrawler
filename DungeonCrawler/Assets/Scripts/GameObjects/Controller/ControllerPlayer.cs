using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ControllerPlayer : Controller {

    [EnumFlags]
    public Entities playerEnemies;

    public float forwardSpeed = 0.1f;
    public float leftSpeed = 0.1f;
    public float backSpeed = 0.1f;
    public float rightSpeed = 0.1f;
    public float jumpforce = 1f;

    private bool isGrounded;
    public bool IsGrounded
    {
        get { return isGrounded; }
        set
        {
            isGrounded = value;
            animator.SetBool("GroundContact", IsGrounded);
        }
    }

    [Tooltip("The animator used for this Player. If not supplied, the script will search the transform and it's children")]
    public Animator animator;
    private AttackerPlayer attacker;
    private Rigidbody rigid;
    private CameraMovementController cameraMovementController;

    public Vector3 ForwardDirection { get { return  transform.forward; } }
    public Vector3 LeftDirection    { get { return -transform.right;   } }
    public Vector3 BackDirection    { get { return -transform.forward; } }
    public Vector3 RightDirection   { get { return  transform.right;   } }

    protected override void Awake()
    {
        base.Awake();
        EnemyTypes = Global.GetSelectedEntries(playerEnemies);

        attacker = GetComponent<AttackerPlayer>();
        rigid = GetComponent<Rigidbody>();
        cameraMovementController = GetComponentInChildren<CameraMovementController>();
        animator = GetComponent<Animator>();

        if (animator == null)
            animator = GetComponent<Animator>();
        if (animator == null)
            animator = GetComponentInChildren<Animator>();


        ControllerMethodTrigger[] leftFires = animator.GetBehaviours<ControllerMethodTrigger>();
        for (int index = 0; index < leftFires.Length; index++)
        {
            leftFires[index].Controller = this;
        }
    }

    public override void Jump()
    {
        rigid.AddForce(0, jumpforce, 0);
    }

    public override void UseLeft()
    {
        if (equipmetHolder.LeftHand.HoldableMode == HoldableMode.SingleClick)
        {
            animator.SetBool("UseLeft", false);
            equipmetHolder.LeftHand.Use(this);
        }
        else if (equipmetHolder.LeftHand.HoldableMode == HoldableMode.Hold)
        {
            equipmetHolder.LeftHand.Use(this);
        }
    }

    public override void UseRight()
    {
        if (equipmetHolder.RightHand.HoldableMode == HoldableMode.SingleClick)
        {
            animator.SetBool("UseRight", false);
            equipmetHolder.RightHand.Use(this);
        }
        else if (equipmetHolder.RightHand.HoldableMode == HoldableMode.Hold)
        {
            equipmetHolder.RightHand.Use(this);
        }
    }

    public override void UpdateLeft()
    {
        if (equipmetHolder.LeftHand.HoldableMode == HoldableMode.Hold)
        {
            if (CTRLHub.GM.LeftAttack == false)
            {
                animator.SetBool("UseLeft", false);
                //animator.SetTrigger("InterruptLeft");
                (equipmetHolder.LeftHand as Shield).UpdateUse(false);
            }
        }
    }

    public override void UpdateRight()
    {
        if (equipmetHolder.RightHand.HoldableMode == HoldableMode.Hold)
        {
            if (CTRLHub.GM.RightAttack == false)
            {
                animator.SetBool("UseRight", false);
                //animator.SetTrigger("InterruptRight");
                (equipmetHolder.RightHand as Shield).UpdateUse(false);
            }
        }
    }

    protected override void FixedUpdate()
    {
        if (CTRLHub.GM.Forward)
        {
            SnapPlayerInCameraDirection();
            rigid.AddForce(ForwardDirection * forwardSpeed);
        }

        if (CTRLHub.GM.Left)
        {
            SnapPlayerInCameraDirection();
            rigid.AddForce(LeftDirection* leftSpeed);
        }
        if (CTRLHub.GM.Back)
        {
            SnapPlayerInCameraDirection();
            rigid.AddForce(BackDirection* backSpeed);
        }
        if (CTRLHub.GM.Right)
        {
            SnapPlayerInCameraDirection();
            rigid.AddForce(RightDirection* rightSpeed);
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
