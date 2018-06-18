using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Holdable {

    [Tooltip("The Collider used to calculate a hit. If it isn't supplied, the script will search it's gameObject as well as children (in ths order)")]
    public Collider attackCollider;

    /// <summary>
    /// If no attackCollider is supplied, the script will search the it's gameObject as well as children (in ths order) for an attackCollider"
    /// </summary>
    protected override void Awake()
    {
        if (attackCollider == null)
            attackCollider.GetComponent<Collider>();
        if (attackCollider == null)
            attackCollider.GetComponentInChildren<Collider>();

        base.Awake();
    }

    /// <summary>
    /// Attack!!
    /// </summary>
    public override bool Use()
    {


        return base.Use();
    }
}
