using UnityEngine;

public class SubstanceManager : MonoBehaviour {

    public static SubstanceManager inst;

    [SerializeField]
    private GameObject explosion;

    private void Awake()
    {
        inst = this;
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
                Kristal_Green_Red(reactionLocation);
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
        ParticleSystem explosionParticles = Instantiate(
            inst.explosion, 
            reactionLocation.position, 
            Quaternion.identity, 
            inst.transform).GetComponent<ParticleSystem>();
        explosionParticles.Play();
        Destroy(explosionParticles, explosionParticles.main.duration);
    }

    private static void Smoke_Green_Silver(Transform reactionLocation)
    {
    }

    private static void Kristal_Green_Red(Transform reactionLocation)
    {
    }

}
