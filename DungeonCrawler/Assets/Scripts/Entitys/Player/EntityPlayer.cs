using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntityPlayer : Entity {

    public Text healthDisplay;

    public override float Health
    {
        get { return base.Health; }

        protected set
        {
            base.Health = value;
            healthDisplay.text = "Health: " + Health;
        }
    }

    protected override void KillEntity()
    {
        Destroy(transform.gameObject);
    }

}
