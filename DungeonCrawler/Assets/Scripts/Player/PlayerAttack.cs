using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Controller {

    private Rigidbody Rigid;

    private CameraController CameraController;

    [SerializeField] [Tooltip("The Time before the gun can be used again")]
    private float gunCooldown = 0.3f;

    [SerializeField] [Tooltip("Time player takes to exit aiming after shooting")]
    private float gunAimTimeOut = 5f;

    private HoldablesHandler HoldablesHandler { get; set; }

    private PlayerSubstanceManager PlayerSubstanceManager { get; set; }

    private SubstanceSelector SubstanceSelector { get; set; }

    private int nextAction;

    private bool isLeftWeaponInfused;
    public bool IsLeftWeaponInfused
    {
        get { return isLeftWeaponInfused; }

        private set
        {
            isLeftWeaponInfused = value;
            HoldablesHandler.LeftEquiped.ToggleInfusion(PlayerSubstanceManager.LeftHandSubstance, value);
        }
    }

    private bool isRightWeaponInfused;
    public bool IsRightWeaponInfused
    {
        get { return isRightWeaponInfused; }

        private set
        {
            isRightWeaponInfused = value;
            HoldablesHandler.RightEquiped.ToggleInfusion(PlayerSubstanceManager.RightHandSubstance, value);
        }
    }

    protected override void Awake()
    {
        base.Awake();

        Rigid = GetComponent<Rigidbody>();
        CameraController = GetComponentInChildren<CameraController>(true);
        HoldablesHandler = GetComponent<HoldablesHandler>();
        PlayerSubstanceManager = GetComponent<PlayerSubstanceManager>();
        SubstanceSelector = GetComponentInChildren<SubstanceSelector>(true);

        PlayerSubstanceManager.LeftHandSubstance = Substance.none_physical;
        PlayerSubstanceManager.RightHandSubstance = Substance.none_physical;

    }

    protected override void Update ()
    {
        Animator.SetBool("isFrozen", IsFrozen);

        AttackMovementForce();
        InputProcessing();
        HandleAiming();
        HandleSubstanceToggle();
        HandleAttacks();
        AnimationQueue();
    }
	
    private void AttackMovementForce()
    {
        if(Animator.GetFloat("attackMovementForce")>0)
        {
            Rigid.AddForce(transform.forward * Animator.GetFloat("attackMovementForce"), ForceMode.Impulse);
            Animator.SetFloat("attackMovementForce", 0);
        }
    }

    private void InputProcessing()
    {
        if (CTRLHub.inst.SubstanceKey)
        {
            if (CTRLHub.inst.LeftAttackDown)
                IsLeftWeaponInfused = !IsLeftWeaponInfused;
            if (CTRLHub.inst.RightAttackDown)
                IsRightWeaponInfused = !IsRightWeaponInfused;
        }
        else if (!Animator.GetBool("weaponSwap"))
        {
            Animator.SetBool("attackRight", CTRLHub.inst.RightAttackDown);
            Animator.SetBool("attackRightHold", CTRLHub.inst.RightAttack);
            Animator.SetBool("attackRightRelease", CTRLHub.inst.RightAttackUp);

            Animator.SetBool("attackLeft", CTRLHub.inst.LeftAttackDown);
            Animator.SetBool("attackLeftHold", CTRLHub.inst.LeftAttack);
            Animator.SetBool("attackLeftRelease", CTRLHub.inst.LeftAttackUp);
        }
    }

    private void HandleAiming()
    {
        bool isAiming = Animator.GetInteger("isAiming") != 0;

        CameraController.ToggleCameraAimingPosition(isAiming);

        //counts cooldown down
        if (Animator.GetFloat("gunCooldownLeft") > 0)
            Animator.SetFloat("gunCooldownLeft", Animator.GetFloat("gunCooldownLeft") - 1 * Time.deltaTime);

        //sets time the player continues aiming
        if (isAiming & Animator.GetBool("attackLeftHold") || Animator.GetBool("attackRightHold"))
            Animator.SetFloat("gunWaitingForShoot", gunAimTimeOut);

        //counts the time down the player remains aiming 
        if (Animator.GetFloat("gunWaitingForShoot") > 0)
            Animator.SetFloat("gunWaitingForShoot", Animator.GetFloat("gunWaitingForShoot") - 1 * Time.deltaTime);
    }

    /// <summary>
    /// Toggle the display and the used substance itself
    /// </summary>
    private void HandleSubstanceToggle()
    {
        SubstanceSelector.SubstanceSelectorParts.SetActive(CTRLHub.inst.SubstanceKey);

        if (CTRLHub.inst.SubstanceKey)
        {
            if (CTRLHub.inst.ScrollValue < 0)
                SubstanceSelector.ScrollUp();
            else if (CTRLHub.inst.ScrollValue > 0)
                SubstanceSelector.ScrollDown();

            if (Input.GetKeyDown(KeyCode.E))
                PlayerSubstanceManager.RightHandSubstance = SubstanceSelector.CurrentSelected;

            if (Input.GetKeyDown(KeyCode.Q))
                PlayerSubstanceManager.LeftHandSubstance = SubstanceSelector.CurrentSelected;
        }
    }

    /// <summary>
    /// Pass the attack command to the weapons
    /// </summary>
    private void HandleAttacks()
    {
        if (Animator.GetBool("initializeAttackLeft"))
        {
            Animator.SetBool("initializeAttackLeft", false);

            switch ((HoldableType)Animator.GetInteger("itemHandLeft"))
            {
                default: return;

                case HoldableType.sword:
                    (HoldablesHandler.LeftEquiped as Sword).Attack(PlayerSubstanceManager, PlayerSubstanceManager.LeftHandSubstance, EnemyTypes);
                    break;

                case HoldableType.gun:
                    {
                        (HoldablesHandler.LeftEquiped as Gun).ShootFromHip(PlayerSubstanceManager.LeftHandSubstance, EnemyTypes);
                        Animator.SetFloat("gunCooldownLeft", gunCooldown);
                        Animator.SetFloat("gunWaitingForShoot", gunAimTimeOut);
                    }
                    return;
            }
        }

        if (Animator.GetBool("initializeAttackRight"))
        {
            Animator.SetBool("initializeAttackRight", false);

            switch ((HoldableType)Animator.GetInteger("itemHandRight"))
            {
                default: return;

                case HoldableType.sword:
                    (HoldablesHandler.RightEquiped as Sword).Attack(PlayerSubstanceManager, PlayerSubstanceManager.RightHandSubstance, EnemyTypes);
                    return;

                case HoldableType.gun:
                    (HoldablesHandler.RightEquiped as Gun).ShootFromHip(PlayerSubstanceManager.RightHandSubstance, EnemyTypes);
                    return;
            }
        }
    }

    private void AnimationQueue()
    {
        bool queueAnimation = Animator.GetBool("queueAnimation");
        Animator.SetInteger("nextAction", nextAction);

        if(queueAnimation)
        {
            if (CTRLHub.inst.RightAttackDown)
                nextAction = 1;
            else if (CTRLHub.inst.LeftAttackDown)
                nextAction = 2;
            else if (CTRLHub.inst.Roll)
                nextAction = 3;
            else if (CTRLHub.inst.JumpDown)
                nextAction = 4;
        }

        if(!queueAnimation)
        {
            nextAction = 0;
        }
    }
}
