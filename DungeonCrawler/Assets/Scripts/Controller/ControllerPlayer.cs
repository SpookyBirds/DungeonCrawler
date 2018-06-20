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

    private AttackerPlayer attacker;
    private Rigidbody rigid;
    private CameraMovementController cameraMovementController;
    [Tooltip("The animator used for this Player. If not supplied, the script will search the transform and it's children")]
    public Animator animator;

    public Vector3 ForwardDirection { get { return transform.forward;  } }
    public Vector3 LeftDirection    { get { return -transform.right;   } }
    public Vector3 BackDirection    { get { return -transform.forward; } }
    public Vector3 RightDirection   { get { return transform.right;    } }

    protected override void Awake()
    {
        attacker = GetComponent<AttackerPlayer>();
        rigid = GetComponent<Rigidbody>();
        cameraMovementController = GetComponentInChildren<CameraMovementController>();
        animator = GetComponent<Animator>();

        if (animator == null)
            animator = GetComponent<Animator>();
        if (animator == null)
            animator = GetComponentInChildren<Animator>();

        EnemyTypes = Global.GetSelectedEntries(playerEnemies);

        base.Awake();
    }

    protected override void Update()
    {
        /// Handle animator statemachine logic
        animator.SetBool("LeftUse",  CTRLHub.GM.LeftAttackDown);
        animator.SetBool("RightUse", CTRLHub.GM.RightAttackDown);
        animator.SetBool("Run",      CTRLHub.GM.Forward);
        animator.SetBool("Jump",     CTRLHub.GM.JumpDown);


        /// Handle jumping
        if (CTRLHub.GM.JumpDown)
        {
            // Let the player jump if the groundcheck is true
            if (IsGrounded)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    rigid.AddForce(0, jumpforce, 0);
            }
        }

        base.Update();
    }

    private void UseLeft()
    {

    }

    private void UseRight()
    {
        attacker.StartAttack();
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

        base.FixedUpdate();
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

    private void OnEnable()
    {
        RightUseFire.Fired += UseRight;
        LeftUseFire.Fired += UseLeft;
    }
}
