using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHub : MonoBehaviour
{
    
    public static KeyHub GM;

    public KeyCode attack { get; set; }
    public KeyCode block { get; set; }
    public KeyCode forward { get; set; }
    public KeyCode left { get; set; }
    public KeyCode right { get; set; }
    public KeyCode backward { get; set; }
    public KeyCode jump { get; set; }



    void Awake()
    {
        
        //Singleton pattern
        if (GM == null)
        {
            DontDestroyOnLoad(gameObject);
            GM = this;
        }
        else if (GM != this)
        {
            Destroy(gameObject);
        }
        attack = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Fire1", "Mouse0"));
        block = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Fire2", "Mouse1"));
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("jumpKey", "Space"));
        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("forwardKey", "W"));
        backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("backwardKey", "S"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("leftKey", "A"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("rightKey", "D"));

    }
}
