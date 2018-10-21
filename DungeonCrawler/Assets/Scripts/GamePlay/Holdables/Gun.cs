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
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private int bulletSpeed;

    private GameObject bullet;


    private PointerSupplier pointerSupplier;

    protected override void Awake()
    {
        base.Awake();

        pointerSupplier = Camera.main.GetComponent<PointerSupplier>();
    }
    //function is not in use anymore. the player is not able to shoot up or down
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
        bullet = Instantiate(bulletPrefab, transform.GetChild(0).position, transform.rotation, Global.inst.Drops.transform);
        bullet.GetComponent<Rigidbody>().AddForce(Camera.main.ScreenPointToRay(Input.mousePosition).direction * bulletSpeed);
        bullet.GetComponent<ParticleEffectSelector>().substance = substance;
        bullet.GetComponent<Bullet>().substance = substance;

                    
        //warum return?
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
