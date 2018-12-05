using System;
using System.Reflection;
using UnityEngine;

public class PlayerControllerMethodTrigger : StateMachineBehaviour {

    public State state;

    private delegate void MethodFire(ControllerPlayer controller);
    public ControllerPlayer Controller { get; set; }
    private MethodFire fireEnter;
    private MethodFire fireUpdate;

    //private bool attackStarted = false;

    private void Awake()
    {
        string fireEnterMethodName = "";
        string fireUpdateMethodName  = "";

        if (state == State.UseLeft)
        {
            fireEnterMethodName = "UseLeft";
            fireUpdateMethodName  = "UpdateLeft";
        }
        else if (state == State.UseRight)
        {
            fireEnterMethodName = "UseRight";
            fireUpdateMethodName  = "UpdateRight";
        }
        else if (state == State.Jump)
        {
            fireEnterMethodName = "Jump";
        }

        MethodInfo enterMethodInfo = typeof(ControllerPlayer).GetMethod(fireEnterMethodName, BindingFlags.Public | BindingFlags.Instance);
        fireEnter = (MethodFire)Delegate.CreateDelegate(typeof(MethodFire), null, enterMethodInfo);

        MethodInfo updateMethodInfo = typeof(ControllerPlayer).GetMethod(fireUpdateMethodName, BindingFlags.Public | BindingFlags.Instance);
        if(updateMethodInfo != null)
            fireUpdate = (MethodFire)Delegate.CreateDelegate(typeof(MethodFire), null, updateMethodInfo);
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        fireEnter(Controller);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (state == State.Jump)
            return;

        fireUpdate(Controller);
    }
}