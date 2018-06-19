using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : InheritanceSimplyfier {

    /// <summary>
    /// The Entity types 
    /// </summary>
    private int[] enemyTypes;
    public int[] EnemyTypes
    {
        get { return enemyTypes; }
        protected set { enemyTypes = value; }
    }

}
