using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace ISU.Example
{
    public abstract class VariableCore<T> : ScriptableObject, ISerializationCallbackReceiver
    {
        [Header("作者紀錄")]
        [SerializeField, TextArea(5, 20)] string developerNote;
        [Header("數值")]
        public T initialValue;
        [NonSerialized] public T value;

        public void OnAfterDeserialize()
        {

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
}