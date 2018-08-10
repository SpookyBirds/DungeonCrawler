﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Entity))]
public abstract class Controller : InheritanceSimplyfier {

    [EnumFlags] [SerializeField] [Tooltip("All enemy types in hostility with this one")]
    private Entities hostileEntities;
    public Entities HostileEntities
    {
        get { return hostileEntities; }
        protected set { hostileEntities = value; }
    }

    [SerializeField] [Tooltip("The animator used for this Entity. If not supplied, the script will search the transform and it's children")]
    private Animator animator;
    public Animator Animator
    {
        get { return animator; }
        protected set { animator = value; }
    }


    /// <summary>
    /// The entity controlled by this controller
    /// </summary>
    public Entity Entity { get; protected set; }

    /// <summary>
    /// The Entity types 
    /// </summary>
    private int[] enemyTypes;
    public int[] EnemyTypes
    {
        get { return enemyTypes; }
        protected set { enemyTypes = value; }
    }

    /// Entity facing directions
    public Vector3 ForwardDirection { get { return  transform.forward; } }
    public Vector3 LeftDirection    { get { return -transform.right;   } }
    public Vector3 BackDirection    { get { return -transform.forward; } }
    public Vector3 RightDirection   { get { return  transform.right;   } }


    protected override void Awake()
    {
        EnemyTypes = Global.GetSelectedEntries(hostileEntities);
        Entity = GetComponentInChildren<Entity>();
        if (Animator == null)
            Animator = GetComponentInChildren<Animator>();
    }


}