using UnityEngine;

public class Gun_old : Holdable_old
{
    [Space]
    [SerializeField]
    private float maxReach = 10;
    [SerializeField]
    private float damagePerHit = 10;

    private PointerSupplier pointerSupplier;
    public ParticleSystem shotParticle;

    [SerializeField]
    private Substance ammunitionLoad = Substance.none_physical;
    
    // Secures that only one shot is fired
    private bool isAiming = false;
    private bool IsAiming
    {
        get { return isAiming; }
        set
        {
            isAiming = value;
            StartAim(value);
        }
    }

    protected override void Awake()
    {
        base.Awake();

        pointerSupplier = Camera.main.GetComponent<PointerSupplier>();
    }

    public override bool UseLong(Controller controller)
    {
        IsAiming = true;
        return false;
    }

    public override void UpdateUse(Controller controller, bool quit)
    {
        if (quit)
        {
            if (IsAiming)
            {
                IsAiming = false;
                CombatManager.Shoot(
                    pointerSupplier.cameraMovementController.RotationCenterPoint.position,  
                    Camera.main.ScreenPointToRay(Input.mousePosition).direction,
                    maxReach,
                    damagePerHit,
                    ammunitionLoad,
                    controller.EnemyTypes
                );
            }
        }
    }

    public override bool UseShort(Controller controller)
    {
        return CombatManager.Shoot(
            model.position, 
            pointerSupplier.character.forward,
            maxReach,
            damagePerHit,
            ammunitionLoad,
            controller.EnemyTypes);
    }

    private void StartAim(bool doAim)
    {
        pointerSupplier.cameraMovementController.ToggleCameraAimingPosition(doAim);
    }

    void ShotingParticles()
    {
        //Starts spawning the particles when the player shoots
        shotParticle.Play();
    }


    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.cyan;
    //    Gizmos.DrawRay(transform.position, pointerSupplier.character.forward * maxReach);
    //    Gizmos.DrawRay(
    //        pointerSupplier.cameraMovementController.RotationCenterPoint.position,
    //        Camera.main.ScreenPointToRay(Input.mousePosition).direction * maxReach);
    //}
}
