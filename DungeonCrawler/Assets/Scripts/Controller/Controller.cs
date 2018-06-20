using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EquipmetHolder))]
public abstract class Controller : InheritanceSimplyfier {

    protected EquipmetHolder equipmetHolder;

    /// <summary>
    /// The Entity types 
    /// </summary>
    private int[] enemyTypes;
    public int[] EnemyTypes
    {
        get { return enemyTypes; }
        protected set { enemyTypes = value; }
    }

    protected override void Awake()
    {
        equipmetHolder = GetComponent<EquipmetHolder>();
    }

    /// <summary>
    /// Use the holdable in the right hand. Returns whether it was successfull
    /// </summary>
    public virtual void UseRight()
    {
    }

    /// <summary>
    /// Use the holdable in the left hand. Returns whether it was successfull
    /// </summary>
    public virtual void UseLeft()
    {
    }

}
