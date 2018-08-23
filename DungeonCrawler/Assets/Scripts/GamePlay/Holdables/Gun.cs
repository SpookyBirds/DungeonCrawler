using UnityEngine;

public class Gun : Holdable
{
    [Space]
    [SerializeField]
    private float maxReach = 10;
    [SerializeField]
    private float damagePerHit = 10;
    [SerializeField]
    private ParticleSystem shotParticle;

    private PointerSupplier pointerSupplier;

    protected override void Awake()
    {
        base.Awake();

        pointerSupplier = Camera.main.GetComponent<PointerSupplier>();
    }

    public bool ShootFromHip(Substance substance, int[] enemyTypes)
    {
        ShotingParticles();
        return CombatManager.Shoot(
            model.position,
            pointerSupplier.character.forward,
            maxReach,
            damagePerHit,
            substance,
            enemyTypes);
    }

    public bool ShootAiming(Substance substance, int[] enemyTypes)
    {
        ShotingParticles();
        return CombatManager.Shoot(
                    pointerSupplier.cameraMovementController.RotationCenterPoint.position,
                    Camera.main.ScreenPointToRay(Input.mousePosition).direction,
                    maxReach,
                    damagePerHit,
                    substance,
                    enemyTypes);
    }

    void ShotingParticles()
    {
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
