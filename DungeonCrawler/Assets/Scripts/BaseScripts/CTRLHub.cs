using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CTRLHub : MonoBehaviour
{
    public float timeUntilLongFire = 0.35f;

    public static CTRLHub inst;

    public float HorizontalAxis { get { return Input.GetAxis("Horizontal"); } }
    public float VerticalAxis   { get { return Input.GetAxis("Vertical");   } }

    public KeyCode ForwardKeyCode { get; set; }
    public bool ForwardDown { get { return (Input.GetKeyDown(ForwardKeyCode)); } }
    public bool ForwardUp   { get { return (Input.GetKeyUp(ForwardKeyCode));   } }
    public bool Forward     { get { return (Input.GetKey(ForwardKeyCode));     } }

    public KeyCode LeftKeyCode { get; set; }
    public bool LeftDown { get { return (Input.GetKeyDown(LeftKeyCode)); } }
    public bool LeftUp   { get { return (Input.GetKeyUp(LeftKeyCode));   } }
    public bool Left     { get { return (Input.GetKey(LeftKeyCode));     } }

    public KeyCode RightKeyCode { get; set; }
    public bool RightDown { get { return (Input.GetKeyDown(RightKeyCode)); } }
    public bool RightUp   { get { return (Input.GetKeyUp(RightKeyCode));   } }
    public bool Right     { get { return (Input.GetKey(RightKeyCode));     } }

    public KeyCode BackwardKeyCode { get; set; }
    public bool BackDown { get { return (Input.GetKeyDown(BackwardKeyCode)); } }
    public bool BackUp   { get { return (Input.GetKeyUp(BackwardKeyCode));   } }
    public bool Back     { get { return (Input.GetKey(BackwardKeyCode));     } }

    public KeyCode JumpKeyCode { get; set; }
    public bool JumpDown { get { return (Input.GetKeyDown(JumpKeyCode)); } }
    public bool JumpUp   { get { return (Input.GetKeyUp(JumpKeyCode));   } }
    public bool Jump     { get { return (Input.GetKey(JumpKeyCode));     } }

    public KeyCode RollKeyCode { get; set; }
    public bool Roll { get { return Input.GetKeyDown(KeyCode.LeftControl); } }


    public KeyCode LeftAttackKeyCode { get; set; }
    public bool LeftAttackDown { get { return (Input.GetKeyDown(LeftAttackKeyCode)); } }
    public bool LeftAttackUp   { get { return (Input.GetKeyUp(LeftAttackKeyCode));   } }
    public bool LeftAttack     { get { return (Input.GetKey(LeftAttackKeyCode));     } }

    public KeyCode RightAttackKeyCode { get; set; }
    public bool RightAttackDown { get { return (Input.GetKeyDown(RightAttackKeyCode)); } }
    public bool RightAttackUp   { get { return (Input.GetKeyUp(RightAttackKeyCode));   } }
    public bool RightAttack     { get { return (Input.GetKey(RightAttackKeyCode));     } }

    public bool LeftFireNormal  { get ; private set; }
    public bool LeftFireLong    { get ; private set; }
    public bool LeftFireHold    { get ; private set; }
    public bool RightFireNormal { get; private set;  }
    public bool RightFireLong   { get; private set;  }
    public bool RightFireHold   { get; private set;  }

    private float rightFireTimer;
    private float leftFireTimer;

    public void Awake()
    {
        //Singleton pattern
        if (inst == null)
        {
            inst = this;
        }
        else if (inst != this)
        {
            Destroy(gameObject);
        }

        LeftFireNormal  = false;
        LeftFireLong    = false;
        LeftFireHold    = false;
        RightFireNormal = false;
        RightFireLong   = false;
        RightFireHold   = false;

        LeftAttackKeyCode  = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Fire1",       "Mouse0"));
        RightAttackKeyCode = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Fire2",       "Mouse1"));
        JumpKeyCode        = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey",     "Space" ));
        ForwardKeyCode     = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardKey",  "W"     ));
        LeftKeyCode        = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey",     "A"     ));
        BackwardKeyCode    = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardKey", "S"     ));
        RightKeyCode       = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey",    "D"     ));
    }

    private void Update()
    {
        LeftFireNormal = false;
        LeftFireLong = false;

        if (LeftAttackDown)
        {
            leftFireTimer = 0;
            LeftFireHold = false;
        }

        if (LeftAttack)
            leftFireTimer += Time.deltaTime;

        if ((leftFireTimer) <= timeUntilLongFire)
        {
            if (LeftAttackUp)
                LeftFireNormal = true;
        }
        else
        {
            if (LeftAttackUp)
                LeftFireLong = true;

            LeftFireHold = true;
        }

        if (LeftAttackUp)
        {
            LeftFireHold = false;
            leftFireTimer = 0;
        }



        RightFireNormal = false;
        RightFireLong = false;

        if (RightAttackDown)
        {
            rightFireTimer = 0;
            RightFireHold = false;
        }

        if (RightAttack)
            rightFireTimer += Time.deltaTime;

        if (rightFireTimer <= timeUntilLongFire)
        {
            if (RightAttackUp)
                RightFireNormal = true;
        }
        else
        {
            if (RightAttackUp)
                RightFireLong = true;

            RightFireHold = true;
        }

        if (RightAttackUp)
        {
            RightFireHold = false;
            rightFireTimer = 0;
        }
    }
}
