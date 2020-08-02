using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class VariableCore<T> : ScriptableObject
{
    public T staticValue;
    public T value
    {
        get
        {
            if (!useStaticValue)
            {
                return _value;
            }
            else
            {
                return staticValue;
            }
        }
        set
        {
            if (!useStaticValue)
            {
                _value = value;
            }
            else
            {

            }
        }
    }
    T _value;
    [SerializeField] protected bool useStaticValue;
}
