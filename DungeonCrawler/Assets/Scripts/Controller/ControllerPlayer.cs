using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ControllerPlayer : Controller {

    [EnumFlags]
    public Entities playerEnemies;

    public KeyCode attackingKey = KeyCode.Mouse0;

    public float forwardSpeed = 0.1f;
    public float leftSpeed = 0.1f;
    public float backSpeed = 0.1f;
    public float rightSpeed = 0.1f;
    public float jumpforce = 1f;

    private bool isGrounded;

    private AttackerPlayer attacker;
    private Rigidbody rigid;
    private CameraMovementController cameraMovementController;

    public Vector3 ForwardDirection { get { return transform.forward;  } }
    public Vector3 LeftDirection    { get { return -transform.right;   } }
    public Vector3 BackDirection    { get { return -transform.forward; } }
    public Vector3 RightDirection   { get { return transform.right;    } }

    protected override void Awake()
    {
        attacker = GetComponent<AttackerPlayer>();
        rigid = GetComponent<Rigidbody>();
        cameraMovementController = GetComponentInChildren<CameraMovementController>();

        enemyTypes = Global.GetSelectedEntries(playerEnemies);

        base.Awake();
    }

    protected override void Update()
    {
        /// Handle attacking
        if (Input.GetKeyDown(attackingKey))
        {
            attacker.StartAttack();
        }

        /// Handle jumping
        //if (KeyHub)
        //{
        //    // Let the player jump if the groundcheck is true
        //    if (Groundcheck == true)
        //    {
        //        if (Input.GetKeyDown(KeyCode.Space))
        //            rigid.AddForce(0, jumpforce, 0);
        //    }
        //}

        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        //if (Input.GetKey(GM.Forward))
        //{
        //    SnapPlayerInCameraDirection();
        //    transform.position += ForwardDirection * forwardSpeed;
        //}
        //if (Input.GetKey(GM.Left))
        //{
        //    SnapPlayerInCameraDirection();
        //    transform.position += LeftDirection * leftSpeed;
        //}
        //if (Input.GetKey(GM.Backward))
        //{
        //    SnapPlayerInCameraDirection();
        //    transform.position += BackDirection * backSpeed;
        //}
        //if (Input.GetKey(GM.Right))
        //{
        //    SnapPlayerInCameraDirection();
        //    transform.position += RightDirection * rightSpeed;
        //}

    }
}
