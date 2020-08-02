using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SerializableDictionary<TKey, TValue> : ISerializationCallbackReceiver
{
    public List<TKey> _keys;
    public List<TValue> _values;
    public Dictionary<TKey, TValue> _dictionary = new Dictionary<TKey, TValue>();
    public Dictionary<TKey, TValue> value() => _dictionary;
    public void OnAfterDeserialize()
    {
        _dictionary = new Dictionary<TKey, TValue>();
        for (int i = 0; i < Mathf.Min(_keys.Count, _values.Count); i++)
        {
            Debug.Log($"key{_keys[i]}  value{_values[i]}");
            _dictionary.Add(_keys[i], _values[i]);
        }
    }
    public void OnBeforeSerialize()
    {
        _keys.Clear();
        _values.Clear();
        foreach (var keyValue in _dictionary)
        {
            _keys.Add(keyValue.Key);
            _values.Add(keyValue.Value);
        }
    }
}
