using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CTRLHub : MonoBehaviour
{
    public static CTRLHub inst;

    public KeyCode LeftAttackKeyCode { get; set; }
    public bool LeftAttackDown { get { return (Input.GetKeyDown(LeftAttackKeyCode)); } }
    public bool LeftAttack { get { return (Input.GetKey(LeftAttackKeyCode)); } }

    public KeyCode RightAttackKeyCode { get; set; }
    public bool RightAttackDown { get { return (Input.GetKeyDown(RightAttackKeyCode)); } }
    public bool RightAttack { get { return (Input.GetKey(RightAttackKeyCode)); } }

    public KeyCode ForwardKeyCode { get; set; }
    public bool ForwardDown { get { return (Input.GetKeyDown(ForwardKeyCode)); } }
    public bool Forward { get { return (Input.GetKey(ForwardKeyCode)); } }

    public KeyCode LeftKeyCode { get; set; }
    public bool LeftDown { get { return (Input.GetKeyDown(LeftKeyCode)); } }
    public bool Left { get { return (Input.GetKey(LeftKeyCode)); } }

    public KeyCode RightKeyCode { get; set; }
    public bool RightDown { get { return (Input.GetKeyDown(RightKeyCode)); } }
    public bool Right { get { return (Input.GetKey(RightKeyCode)); } }

    public KeyCode BackwardKeyCode { get; set; }
    public bool BackDown { get { return (Input.GetKeyDown(BackwardKeyCode)); } }
    public bool Back { get { return (Input.GetKey(BackwardKeyCode)); } }

    public KeyCode JumpKeyCode { get; set; }
    public bool JumpDown { get { return (Input.GetKeyDown(JumpKeyCode)); } }
    public bool Jump { get { return (Input.GetKey(JumpKeyCode)); } }

    

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

        LeftAttackKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Fire1", "Mouse0"));
        RightAttackKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Fire2", "Mouse1"));
        JumpKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "Space"));
        ForwardKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardKey", "W"));
        BackwardKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardKey", "S"));
        LeftKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "A"));
        RightKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));
    }
}
