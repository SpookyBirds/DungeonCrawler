using UnityEngine;

public class SubstanceManager : MonoBehaviour {

    public static SubstanceManager inst;

    [EnumFlags] [SerializeField] [Tooltip("All entities that are affected by the substances")]
    private Entities effected;
    [Space]
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private float explosionRadius;
    [SerializeField]
    private float explosionDamage;
    [Space]
    [SerializeField]
    private GameObject crystal;

    public int[] Effected { get; protected set; }

    private void Awake()
    {
        inst = this;
        Effected = Global.GetSelectedEntries(effected);
    }

    public static void ReactSubstances(Substance infusedSubstance, Substance attackedSubstance, Transform reactionLocation)
    {
        if (infusedSubstance == attackedSubstance)
            return;
                 
        int reactionCombination = (int)infusedSubstance * (int)attackedSubstance;

        switch (reactionCombination)
        {
            case 0: return;
            default: return;

            case 6: // green(2) and red(3)
                Crystal_Green_Red(reactionLocation);
                break;

            case 10: // green(2) and silber(5)
                Smoke_Green_Silver(reactionLocation);
                break;

            case 15: // red(3) and silver(5)
                Explosion_Red_Silver(reactionLocation);
                break;
        }
    }

    private static void Explosion_Red_Silver(Transform reactionLocation)
    {
        // Particles

        ParticleSystem explosionParticles = Instantiate(
            inst.explosion, 
            reactionLocation.position, 
            Quaternion.identity, 
            inst.transform).GetComponent<ParticleSystem>();
        explosionParticles.Play();
        Destroy(explosionParticles.gameObject, explosionParticles.main.duration);

        // Damage

        CombatManager.ColliderAttackSphere(
            reactionLocation.position, 
            inst.explosionRadius, 
            inst.explosionDamage, 
            Substance.none_physical, 
            inst.Effected);
    }

    private static void Smoke_Green_Silver(Transform reactionLocation)
    {
    }

    private static void Crystal_Green_Red(Transform reactionist)
    {
        // Particles and instanziation

        ParticleSystem crystalParticles;
        if (reactionist.CompareTag(Global.NeutralTag)) 
        {
            crystalParticles = Instantiate( inst.crystal, reactionist.position, Quaternion.identity, inst.transform)
                .GetComponent<ParticleSystem>();
        }
        else
        {
            crystalParticles = Instantiate(inst.crystal, reactionist).GetComponent<ParticleSystem>();
        }
        crystalParticles.Play();
        Destroy(crystalParticles.gameObject, crystalParticles.main.duration);

        // Freezing

        SphereCollider crystalArea = crystalParticles.GetComponent<SphereCollider>();
        Collider[] frozenCollider = Physics.OverlapSphere(crystalArea.transform.position, crystalArea.radius);

        for (int index = 0; index < frozenCollider.Length; index++)
        {
            if (frozenCollider[ index ].IsAnyTag(Global.Npcs))
                frozenCollider[ index ].GetComponent<NPC_AI>().Freeze(crystalParticles.main.duration);
            else if (frozenCollider[ index ].CompareTag(Global.PlayerTag))
                frozenCollider[ index ].GetComponent<ControllerPlayer>().Freeze(crystalParticles.main.duration);
        }
    }
}
