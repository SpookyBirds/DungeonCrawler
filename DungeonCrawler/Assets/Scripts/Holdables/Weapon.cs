using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Holdable {

    public float damagePerHit;
    public float castingTimeInSeconds;
    [SerializeField] [Tooltip("The Collider used to calculate a hit. If it isn't supplied, the script will search it's gameObject as well as children (in ths order)")]
    private Collider attackCollider;

    public Vector3 AttackColliderPosition { get { return attackCollider.transform.position; } }
    public float AttackRange { get { return attackCollider.bounds.extents.z; } }
    public Controller controller;

    /// <summary>
    /// If no attackCollider is supplied, the script will search the it's gameObject as well as children (in ths order) for an attackCollider"
    /// </summary>
    protected override void Awake()
    {
        if (attackCollider == null)
            attackCollider.GetComponent<Collider>();
        if (attackCollider == null)
            attackCollider.GetComponentInChildren<Collider>();

        controller = GetComponent<Controller>();

        base.Awake();
    }

    /// <summary>
    /// Initialize an attack. Returns whether the attack was successfully started
    /// </summary>
    public override bool Use(Controller controller)
    {
        if (AlreadyUsing == false)
        {
            StartCoroutine(Attack(controller));
            return true;
        }

        return false;
    }

    private IEnumerator Attack(Controller controller)
    {
        AlreadyUsing = true;

        yield return new WaitForSeconds(castingTimeInSeconds);

        Collider[] colliderInAttackRange =
            Physics.OverlapBox(attackCollider.bounds.center, attackCollider.bounds.extents);

        for (int index = 0; index < colliderInAttackRange.Length; index++)
        {
            if (colliderInAttackRange[index].IsAnyTagEqual(controller.EnemyTypes))
            {
                float remainingHealth = colliderInAttackRange[index].GetComponent<Entity>().Damage(damagePerHit);
            }
        }

        AlreadyUsing = false;
    }


}
