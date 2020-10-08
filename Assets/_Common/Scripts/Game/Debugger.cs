using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger
{
    public static Func<bool> m_DebugLogOn;

    public static void DebugLog(string _messege)
    {
        if (m_DebugLogOn()) Debug.Log(_messege);
    }
}
