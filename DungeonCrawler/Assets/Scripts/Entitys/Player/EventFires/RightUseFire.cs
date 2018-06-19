using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RightUseFire : StateMachineBehaviour {

    public static UnityAction Fired;

    private void Awake()
    {
        Fired += delegate { };
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Fired.Invoke();
	}
}
