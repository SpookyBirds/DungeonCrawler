﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fist : Holdable
{
    public override bool UseLong(Controller controller)
    {
        return false;
    }

    public override bool UseShort(Controller controller)
    {
        return false;
    }
}
