using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PlayerAnimationQuitTrigger : StateMachineBehaviour {

    public State state;
    public UseType useType;

    private delegate void MethodFire(ControllerPlayer controller, UseType useType);
    public ControllerPlayer Controller { get; set; }
    private MethodFire fire;

    public ValueWrapper<bool> hasStarted;

    private void Awake()
    {
        string fireEnterMethodName = "";

        if (state == State.UseLeft)
        {
            fireEnterMethodName = "QuitLeft";
        }
        else if (state == State.UseRight)
        {
            fireEnterMethodName = "QuitRight";
        }
        else if (state == State.Jump)
        {
            fireEnterMethodName = "Jump";
        }

        MethodInfo enterMethodInfo = typeof(ControllerPlayer).GetMethod(fireEnterMethodName, BindingFlags.Public | BindingFlags.Instance);
        fire = (MethodFire)Delegate.CreateDelegate(typeof(MethodFire), null, enterMethodInfo);
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
    {
        hasStarted.Value = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!hasStarted.Value)
            return;

        Debug.Log("FIRE! quit "+ useType);
        fire(Controller, useType);
    }
}