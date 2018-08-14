using UnityEngine;

public class SubstanceManager : MonoBehaviour {

    public static SubstanceManager inst;

    [EnumFlags] [SerializeField] [Tooltip("All entities that are affected by the substances")]
    private Entities effected;
    [Space]
    [SerializeField]
    private GameObject explosionPrefab;
    [SerializeField]
    private float explosionRadius;
    [SerializeField]
    private float explosionDamage;
    [SerializeField]
    private float explosionDuration;
    [Space]
    [SerializeField]
    private GameObject crystalNonPlayerPrefab;
    [SerializeField]
    private float crystalDurationNonPlayer;
    [SerializeField]
    private GameObject crystalPlayerPrefab;
    [SerializeField]
    private float crystalDurationPlayer;
    [Space]
    [SerializeField]
    private GameObject smokePrefab;
    [SerializeField]
    private float smokeDuration;

    public int[] Effected { get; protected set; }

    private void Awake()
    {
        inst = this;
        Effected = Global.GetSelectedEntries(effected);
    }

    public static void ReactSubstances(Substance infusedSubstance, Substance attackedSubstance, Transform reactionist)
    {
        if (infusedSubstance == attackedSubstance)
            return;
                 
        int reactionCombination = (int)infusedSubstance * (int)attackedSubstance;

        switch (reactionCombination)
        {
            case 0: return;
            default: return;

            case 6: // green(2) and red(3)
                Crystal_Green_Red(reactionist);
                break;

            case 10: // green(2) and silber(5)
                Smoke_Green_Silver(reactionist);
                break;

            case 15: // red(3) and silver(5)
                Explosion_Red_Silver(reactionist);
                break;
        }
    }

    private static void Explosion_Red_Silver(Transform reactionist)
    {
        // Particles

        ParticleSystem explosionParticles = Instantiate(
            inst.explosionPrefab, 
            reactionist.position, 
            Quaternion.identity, 
            inst.transform).GetComponent<ParticleSystem>();
        explosionParticles.Play();
        Destroy(explosionParticles.gameObject, inst.explosionDuration);

        // Damage

        CombatManager.ColliderAttackSphere(
            reactionist.position, 
            inst.explosionRadius, 
            inst.explosionDamage, 
            Substance.none_physical, 
            inst.Effected);
    }

    private static void Smoke_Green_Silver(Transform reactionist)
    {             
        // Particles

        ParticleSystem smokeParticles = Instantiate(
            inst.smokePrefab,
            reactionist.position,
            Quaternion.identity,
            inst.transform).GetComponent<ParticleSystem>();
        smokeParticles.Play();
        Destroy(smokeParticles.gameObject, inst.smokeDuration);

        if (reactionist.IsAnyTag(Global.Npcs))
            reactionist.GetComponent<NPC_AI>().SwitchToIdleBaseState();
    }

    private static void Crystal_Green_Red(Transform reactionist)
    {
        // Particles and instanziation

        ParticleSystem crystalParticles = Instantiate(inst.crystalNonPlayerPrefab, reactionist.position, Quaternion.identity, inst.transform)
                .GetComponent<ParticleSystem>();

        if (reactionist.CompareTag(Global.NeutralTag)) 
        {
            Destroy(crystalParticles.gameObject, inst.crystalDurationNonPlayer);
        }   
        else
        {
            TransformSync transformSync = crystalParticles.gameObject.AddComponent<TransformSync>() as TransformSync;
            transformSync.SyncTransform = reactionist;                                                       
            Physics.IgnoreCollision(reactionist.GetComponent<Collider>(), crystalParticles.GetComponent<Collider>());

            if (reactionist.CompareTag(Global.PlayerTag))
            {
                reactionist.GetComponent<ControllerPlayer>().Freeze(crystalParticles.main.duration);
                Destroy(crystalParticles.gameObject, inst.crystalDurationPlayer);
            }
            else
            {
                reactionist.GetComponent<NPC_AI>().Freeze(crystalParticles.main.duration);
                Destroy(crystalParticles.gameObject, inst.crystalDurationNonPlayer);
            }
        }
        crystalParticles.Play();

        // Freezing

        SphereCollider crystalArea = crystalParticles.GetComponent<SphereCollider>();                                                                                                           
        Collider[] frozenCollider = Physics.OverlapSphere(crystalArea.transform.position, crystalArea.radius);

        for (int index = 0; index < frozenCollider.Length; index++)
        {
            Debug.Log(frozenCollider[ index ].name);

            if (frozenCollider[ index ].IsAnyTag(Global.Npcs))
                frozenCollider[ index ].GetComponent<NPC_AI>().Freeze(crystalParticles.main.duration);
            else if (frozenCollider[ index ].CompareTag(Global.PlayerTag))
            {
                frozenCollider[ index ].GetComponent<ControllerPlayer>().Freeze(crystalParticles.main.duration);
                Debug.Log("player freeze -1");
            }
        }
    }
}
