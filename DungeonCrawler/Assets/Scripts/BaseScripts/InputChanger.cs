using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputChanger : MonoBehaviour {

    Transform menuPanel;
    GameObject Test;
    Event keyEvent;
    Text buttonText;
    KeyCode newKey;

    bool waitingForKey;

    private KeyCode[] allCodesInUse;

    public void Start()
    {
        BakeKeyCodeArray();
       
        menuPanel = transform.Find("Panel");

        waitingForKey = false;


        for (int i = 0; i < menuPanel.childCount; i++)
        {

            if (menuPanel.GetChild(i).name == "ForwardKey")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = CTRLHub.inst.ForwardKeyCode.ToString();
            else if (menuPanel.GetChild(i).name == "BackwardKey")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = CTRLHub.inst.BackwardKeyCode.ToString();
            else if (menuPanel.GetChild(i).name == "LeftKey")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = CTRLHub.inst.LeftKeyCode.ToString();
            else if (menuPanel.GetChild(i).name == "RightKey")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = CTRLHub.inst.RightKeyCode.ToString();
            else if (menuPanel.GetChild(i).name == "JumpKey")
                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = CTRLHub.inst.JumpKeyCode.ToString();
        }
    }

    private void BakeKeyCodeArray()
    {
        allCodesInUse = new KeyCode[]
        {
            CTRLHub.inst.LeftAttackKeyCode,
            CTRLHub.inst.RightAttackKeyCode,
            CTRLHub.inst.ForwardKeyCode,
            CTRLHub.inst.LeftKeyCode,
            CTRLHub.inst.RightKeyCode,
            CTRLHub.inst.BackwardKeyCode,
            CTRLHub.inst.JumpKeyCode
        };
    }

    void OnGUI()
    {
        /*keyEvent dictates what key our user presses
         * bt using Event.current to detect the current
         * event
         */
        keyEvent = Event.current;

        //Executes if a button gets pressed and
        //the user presses a key
        if (keyEvent.isKey && waitingForKey)
        {
            newKey = keyEvent.keyCode; //Assigns newKey to the key user presses
            waitingForKey = false;
        }
    }

    /*Buttons cannot call on Coroutines via OnClick().
     * Instead, we have it call StartAssignment, which will
     * call a coroutine in this script instead, only if we
     * are not already waiting for a key to be pressed.
     */
    public void StartAssignment(string keyName)
    {
        if (!waitingForKey)
            StartCoroutine(AssignKey(keyName));
    }

    //Assigns buttonText to the text component of
    //the button that was pressed
    public void SendText(Text text)
    {
        buttonText = text;
    }

    //Used for controlling the flow of our below Coroutine
    IEnumerator WaitForKey()
    {
        while (!keyEvent.isKey)
            yield return null;
    }

    /*AssignKey takes a keyName as a parameter. The
     * keyName is checked in a switch statement. Each
     * case assigns the command that keyName represents
     * to the new key that the user presses, which is grabbed
     * in the OnGUI() function, above.
     */
    public IEnumerator AssignKey(string keyName)
    {
        waitingForKey = true;

        yield return WaitForKey(); //Executes endlessly until user presses a key

        if (KeyAlreadyExisting(newKey) == false)
        {

            switch (keyName)
            {
                case "forward":

                    CTRLHub.inst.ForwardKeyCode = newKey; //Set forward to new keycode
                    buttonText.text = CTRLHub.inst.ForwardKeyCode.ToString(); //Set button text to new key
                    PlayerPrefs.SetString("forwardKey", CTRLHub.inst.ForwardKeyCode.ToString()); //save new key to PlayerPrefs
                    break;

                case "backward":

                    CTRLHub.inst.BackwardKeyCode = newKey; //set backward to new keycode
                    buttonText.text = CTRLHub.inst.BackwardKeyCode.ToString(); //set button text to new key
                    PlayerPrefs.SetString("backwardKey", CTRLHub.inst.BackwardKeyCode.ToString()); //save new key to PlayerPrefs
                    break;

                case "left":

                    CTRLHub.inst.LeftKeyCode = newKey; //set left to new keycode
                    buttonText.text = CTRLHub.inst.LeftKeyCode.ToString(); //set button text to new key
                    PlayerPrefs.SetString("leftKey", CTRLHub.inst.LeftKeyCode.ToString()); //save new key to playerprefs
                    break;

                case "right":

                    CTRLHub.inst.RightKeyCode = newKey; //set right to new keycode
                    buttonText.text = CTRLHub.inst.RightKeyCode.ToString(); //set button text to new key
                    PlayerPrefs.SetString("rightKey", CTRLHub.inst.RightKeyCode.ToString()); //save new key to playerprefs
                    break;

                case "jump":

                    CTRLHub.inst.JumpKeyCode = newKey; //set jump to new keycode
                    buttonText.text = CTRLHub.inst.JumpKeyCode.ToString(); //set button text to new key
                    PlayerPrefs.SetString("jumpKey", CTRLHub.inst.JumpKeyCode.ToString()); //save new key to playerprefs
                    break;
            }

            BakeKeyCodeArray();
        }

        yield return null;
    }

    private bool KeyAlreadyExisting(KeyCode keyToTest)
    {
        for (int index = 0; index < allCodesInUse.Length; index++)
        {
            if (allCodesInUse[index] == keyToTest)
                return true;
        }

        return false;
    }
}
