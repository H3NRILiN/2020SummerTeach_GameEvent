using UnityEngine;
using UnityEngine.Events;
using System;
using ISU.Lesson.GameEvent;

[Serializable]
public class UnityVoidEvent : UnityEvent<Void> { }

[Serializable]
public class UnityTransformEvent : UnityEvent<Transform> { }

[Serializable]
public class UnityHandHoldObjectEvent : UnityEvent<HandHoldObject> { }
