using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    [SerializeField]
    private float standartVisionRadius = 10f;
    [SerializeField]
    private float blindedVisionRadius = 1f;
    [SerializeField]
    private Transform eyes;
    [SerializeField]
    private float[] obstacleEvadingAngles = { -2f, -1f, 1f, 2f };

    private NPC_AI aI;
    private float visionRadius;

    private void Awake()
    {
        aI = GetComponent<NPC_AI>();

        visionRadius = standartVisionRadius;
    }

    /// <summary>
    /// Checks for enemies in a sphere around it's transform with the radius "visionRadius"
    /// </summary>
    /// <returns>Whether or not an enemy was found in the vision radius</returns>
    public bool FindEnemy(int[] enemyTypes)
    {
        Entity transform;
        return FindEnemy(enemyTypes, out transform);
    }

    /// <summary>
    /// Checks for enemies in a sphere around it's transform with the radius "visionRadius"
    /// </summary>
    /// <param name="opponent">The transform of the opponent that was first found</param>
    /// <returns>Whether or not an enemy was found in the vision radius</returns>
    public bool FindEnemy(int[] enemyTypes, out Entity opponent)
    {
        Collider[] colliderInVisionRange =
            Physics.OverlapSphere(transform.position, visionRadius);

        for (int index = 0; index < colliderInVisionRange.Length; index++)
        {
            if (colliderInVisionRange[index].IsAnyTag(enemyTypes))
            {
                opponent = colliderInVisionRange[index].GetComponent<Entity>();

                if (CanSeeOpponent(opponent, 0))
                    return true;

                for (int iteration = 0; iteration < obstacleEvadingAngles.Length; iteration++)
                {
                    if (CanSeeOpponent(opponent, iteration))
                        return true;
                }

            }
        }

        opponent = null;
        return false;
    }

    /// <summary>
    /// Raycast from the eye to the given opponent. Returns true if the opponent was seen
    /// </summary>
    /// <param name="angleFromMiddle">The euler angle by which the raycast is departed</param>
    private bool CanSeeOpponent(Entity opponent, float angleFromMiddle)
    {
        RaycastHit hit;
        if (Physics.Raycast(
            eyes.position, 
            Quaternion.Euler(0, angleFromMiddle, 0) * ((opponent.transform.position + new Vector3(0, 1, 0)) - eyes.position),
            out hit, 
            visionRadius))
        {
            return hit.transform.Equals(opponent.transform);
        }

        return false;
    }

    public void ApplyBlind()
    {
        visionRadius = blindedVisionRadius;
    }

    public void RemoveBlind()
    {
        visionRadius = standartVisionRadius;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
    }
}
