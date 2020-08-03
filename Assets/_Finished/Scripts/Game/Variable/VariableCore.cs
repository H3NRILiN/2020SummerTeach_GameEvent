using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class VariableCore<T> : ScriptableObject, ISerializationCallbackReceiver
{
    public T initialValue;
    [NonSerialized] public T value;

    public void OnAfterDeserialize()
    {
        value = initialValue;
    }

    public void OnBeforeSerialize()
    {

    }
}

public abstract class ListVariableCore<T> : ScriptableObject, ISerializationCallbackReceiver
{
    public List<T> initialValue;
    [NonSerialized] public List<T> value;

    public void OnAfterDeserialize()
    {
        value = new List<T>(initialValue);
    }

    public void OnBeforeSerialize()
    {

    }
}
