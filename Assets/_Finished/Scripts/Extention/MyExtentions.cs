using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MyExtentions
{
    public static void SetActiveNoGC(this GameObject gameObject, bool active)
    {
        if (gameObject.activeSelf != active)
        {
            gameObject.SetActive(active);
            Debug.Log("A");
        }
    }
}
