using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityPlayer : Entity {

    protected override void KillEntity()
    {
        Destroy(transform.gameObject);
    }

}
