using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class VariableCore<T> : ScriptableObject
{
    public T customValue;
    [NonSerialized] public T value;
    [SerializeField] bool usingCustomValue;
}
