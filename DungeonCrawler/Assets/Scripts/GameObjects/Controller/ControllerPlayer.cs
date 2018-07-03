using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ControllerPlayer : Controller {

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
            Animator.SetBool("GroundContact", IsGrounded);
        }
    }

    private CameraMovementController cameraMovementController;

    protected override void Awake()
    {
        base.Awake();

        cameraMovementController = GetComponentInChildren<CameraMovementController>();
    }

    public override void Jump()
    {
        Rigid.AddForce(0, jumpforce, 0);
    }

    public override void UseLeft()
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

    public override void UseRight()
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

    public override void QuitLeft()
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

    public override void QuitRight()
    {
        if (EquipmetHolder.RightHand.HoldableMode == HoldableMode.Hold)
        {
            if (CTRLHub.inst.RightAttack == false)
            {
                Animator.SetBool("UseRight", false);
                (EquipmetHolder.RightHand as Shield).UpdateUse(this, true);
            }
            else
                (EquipmetHolder.LeftHand as Shield).UpdateUse(this, false);
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
