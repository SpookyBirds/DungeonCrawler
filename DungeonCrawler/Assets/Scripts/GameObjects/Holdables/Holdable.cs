using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holdable : InheritanceSimplyfier {

    private bool alreadyUsing = false;
    public bool AlreadyUsing
    {
        get { return alreadyUsing; }
        protected set { alreadyUsing = value; }
    }

    /// <summary>
    /// Activates the use action. Returns whether it was successfull
    /// </summary>
    public virtual bool Use(Controller controller)
    {
        return false;
    }
}
