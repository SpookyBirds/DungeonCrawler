using System;
using System.Reflection;
using UnityEngine;

public class ControllerMethodTrigger : StateMachineBehaviour {

    public State state;

    private delegate void MethodFire(Controller controller);
    public Controller Controller { get; set; }
    private MethodFire fireEnter;
    private MethodFire fireUpdate;

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

        MethodInfo enterMethodInfo = typeof(Controller).GetMethod(fireEnterMethodName, BindingFlags.Public | BindingFlags.Instance);
        fireEnter = (MethodFire)Delegate.CreateDelegate(typeof(MethodFire), null, enterMethodInfo);

        MethodInfo updateMethodInfo = typeof(Controller).GetMethod(fireUpdateMethodName, BindingFlags.Public | BindingFlags.Instance);
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

    public enum State
    {
        UseLeft,
        UseRight,
        Jump
    }
}
