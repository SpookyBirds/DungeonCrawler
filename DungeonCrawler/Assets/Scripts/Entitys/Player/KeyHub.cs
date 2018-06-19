using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHub : MonoBehaviour
{
    public static KeyHub GM;

    public KeyCode Attack { get; set; }
    public KeyCode Block { get; set; }
    public KeyCode Forward { get; set; }
    public KeyCode Left { get; set; }
    public KeyCode Right { get; set; }
    public KeyCode Backward { get; set; }
    public KeyCode Jump { get; set; }

    void Awake()
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

        Attack = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Fire1", "Mouse0"));
        Block = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Fire2", "Mouse1"));
        Jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "Space"));
        Forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardKey", "W"));
        Backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardKey", "S"));
        Left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "A"));
        Right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));
    }
}
