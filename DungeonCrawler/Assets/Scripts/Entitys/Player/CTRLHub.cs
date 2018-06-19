using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTRLHub : MonoBehaviour
{
    public static CTRLHub GM;

    public KeyCode AttackKeyCode { get; set; }
    public bool AttackDown { get { return (Input.GetKeyDown(AttackKeyCode)); } }
    public bool Attack { get { return (Input.GetKey(AttackKeyCode)); } }

    public KeyCode BlockKeyCode { get; set; }
    public bool BlockDown { get { return (Input.GetKey(BlockKeyCode)); } }
    public bool Block { get { return (Input.GetKey(BlockKeyCode)); } }

    public KeyCode ForwardKeyCode { get; set; }
    public bool ForwardDown { get { return (Input.GetKey(ForwardKeyCode)); } }
    public bool Forward { get { return (Input.GetKey(ForwardKeyCode)); } }

    public KeyCode LeftKeyCode { get; set; }
    public bool LeftDown { get { return (Input.GetKey(LeftKeyCode)); } }
    public bool Left { get { return (Input.GetKey(LeftKeyCode)); } }

    public KeyCode RightKeyCode { get; set; }
    public bool RightDown { get { return (Input.GetKey(RightKeyCode)); } }
    public bool Right { get { return (Input.GetKey(RightKeyCode)); } }

    public KeyCode BackwardKeyCode { get; set; }
    public bool BackDown { get { return (Input.GetKey(BackwardKeyCode)); } }
    public bool Back { get { return (Input.GetKey(BackwardKeyCode)); } }

    public KeyCode JumpKeyCode { get; set; }
    public bool JumpDown { get { return (Input.GetKey(JumpKeyCode)); } }
    public bool Jump { get { return (Input.GetKey(JumpKeyCode)); } }


    public void Awake()
    {
        //Singleton pattern
        if (GM == null)
        {
            GM = this;
        }
        else if (GM != this)
        {
            Destroy(gameObject);
        }

        AttackKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Fire1", "Mouse0"));
        BlockKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Fire2", "Mouse1"));
        JumpKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "Space"));
        ForwardKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardKey", "W"));
        BackwardKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardKey", "S"));
        LeftKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "A"));
        RightKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));
    }
}
