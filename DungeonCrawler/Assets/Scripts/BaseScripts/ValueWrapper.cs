using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValueWrapper<T> where T : struct
{
    public T Value { get; set; }

    public ValueWrapper()
    {
        Value = default(T);
    }

    public ValueWrapper(T value)
    {
        Value = value;
    }
}
