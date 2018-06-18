using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holdable : InheritanceSimplyfier {

    /// <summary>
    /// Activates the use action. Returns whether it was successfull
    /// </summary>
    public virtual bool Use()
    {
        return false;
    }
}
