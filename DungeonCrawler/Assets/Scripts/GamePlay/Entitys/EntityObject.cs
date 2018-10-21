using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityObject : Entity {

    // Update is called once per frame
    protected override void Update () {

        if (animator.GetBool("Death"))
        {
            Destroy(gameObject);
        }
	}
}
