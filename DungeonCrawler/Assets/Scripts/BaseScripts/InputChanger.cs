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





    public void Start()

    {

       

        menuPanel = transform.Find("Panel");



        waitingForKey = false;

        


        for (int i = 0; i < menuPanel.childCount; i++)

        {

            if (menuPanel.GetChild(i).name == "ForwardKey")

                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = CTRLHub.GM.ForwardKeyCode.ToString();

            else if (menuPanel.GetChild(i).name == "BackwardKey")

                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = CTRLHub.GM.BackwardKeyCode.ToString();

            else if (menuPanel.GetChild(i).name == "LeftKey")

                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = CTRLHub.GM.LeftKeyCode.ToString();

            else if (menuPanel.GetChild(i).name == "RightKey")

                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = CTRLHub.GM.RightKeyCode.ToString();

            else if (menuPanel.GetChild(i).name == "JumpKey")

                menuPanel.GetChild(i).GetComponentInChildren<Text>().text = CTRLHub.GM.JumpKeyCode.ToString();

        }

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



        switch (keyName)

        {

            case "forward":

                CTRLHub.GM.ForwardKeyCode = newKey; //Set forward to new keycode
                buttonText.text = CTRLHub.GM.ForwardKeyCode.ToString(); //Set button text to new key
                PlayerPrefs.SetString("forwardKey", CTRLHub.GM.ForwardKeyCode.ToString()); //save new key to PlayerPrefs
                break;

            case "backward":

                CTRLHub.GM.BackwardKeyCode = newKey; //set backward to new keycode
                buttonText.text = CTRLHub.GM.BackwardKeyCode.ToString(); //set button text to new key
                PlayerPrefs.SetString("backwardKey", CTRLHub.GM.BackwardKeyCode.ToString()); //save new key to PlayerPrefs
                break;

            case "left":

                CTRLHub.GM.LeftKeyCode = newKey; //set left to new keycode
                buttonText.text = CTRLHub.GM.LeftKeyCode.ToString(); //set button text to new key
                PlayerPrefs.SetString("leftKey", CTRLHub.GM.LeftKeyCode.ToString()); //save new key to playerprefs
                break;

            case "right":

                CTRLHub.GM.RightKeyCode = newKey; //set right to new keycode
                buttonText.text = CTRLHub.GM.RightKeyCode.ToString(); //set button text to new key
                PlayerPrefs.SetString("rightKey", CTRLHub.GM.RightKeyCode.ToString()); //save new key to playerprefs
                break;

            case "jump":

                CTRLHub.GM.JumpKeyCode = newKey; //set jump to new keycode
                buttonText.text = CTRLHub.GM.JumpKeyCode.ToString(); //set button text to new key
                PlayerPrefs.SetString("jumpKey", CTRLHub.GM.JumpKeyCode.ToString()); //save new key to playerprefs
                break;

        }



        yield return null;

    }
}
