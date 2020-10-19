using UnityEngine;
using UnityEngine.Events;
using System;

[Serializable]
public class UnityVoidEvent : UnityEvent<Void> { }

[Serializable]
public class UnityTransformEvent : UnityEvent<Transform> { }
