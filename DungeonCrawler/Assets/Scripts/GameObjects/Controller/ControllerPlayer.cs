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

        rigid = GetComponent<Rigidbody>();
        cameraMovementController = GetComponentInChildren<CameraMovementController>();

    }

    public override void Jump()
    {
        rigid.AddForce(0, jumpforce, 0);
    }

    public override void UseLeft()
    {
        Debug.Log("useleft");

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
        Debug.Log("useright");

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

    public override void QuitLeft()
    {

        Debug.Log("quitleft");
        if (equipmetHolder.LeftHand.HoldableMode == HoldableMode.Hold)
        {
            if (CTRLHub.inst.LeftAttack == false)
            {
                animator.SetBool("UseLeft", false);
                (equipmetHolder.LeftHand as Shield).UpdateUse(this, true);
            }
            else
                (equipmetHolder.LeftHand as Shield).UpdateUse(this, false);
        }
    }

    public override void QuitRight()
    {
        Debug.Log("quitright");
        if (equipmetHolder.RightHand.HoldableMode == HoldableMode.Hold)
        {
            if (CTRLHub.inst.RightAttack == false)
            {                                                                                                                               
                animator.SetBool("UseRight", false);
                (equipmetHolder.RightHand as Shield).UpdateUse(this, true);
            }
            else
                (equipmetHolder.LeftHand as Shield).UpdateUse(this, false);
        }
    }

    protected override void FixedUpdate()
    {
        if (CTRLHub.inst.Forward)
        {
            SnapPlayerInCameraDirection();
            rigid.AddForce(ForwardDirection * forwardSpeed);
        }

        if (CTRLHub.inst.Left)
        {
            SnapPlayerInCameraDirection();
            rigid.AddForce(LeftDirection* leftSpeed);
        }
        if (CTRLHub.inst.Back)
        {
            SnapPlayerInCameraDirection();
            rigid.AddForce(BackDirection* backSpeed);
        }
        if (CTRLHub.inst.Right)
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
