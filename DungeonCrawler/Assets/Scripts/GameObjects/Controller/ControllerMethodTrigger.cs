using System;
using System.Reflection;
using UnityEngine;

public class ControllerMethodTrigger : StateMachineBehaviour {

    private delegate void UseMethod(Controller controller);
    public string methodName;
    [HideInInspector]
    public Controller Controller { get; set; }
    private UseMethod useMethod;
    public Type type;

    private void Awake()
    {
        MethodInfo info = typeof(Controller).GetMethod(methodName, BindingFlags.Public | BindingFlags.Instance);
        useMethod = (UseMethod)Delegate.CreateDelegate(typeof(UseMethod), null, info);
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        useMethod(Controller);
    }
}
