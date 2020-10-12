using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDDebugLog
{
    [System.Diagnostics.Conditional("ACTIVE_DEBUG")]
    public static void Log(string _str)
    {
        Debug.Log(_str);
    }

    [System.Diagnostics.Conditional("ACTIVE_DEBUG")]
    public static void LogWarning(string _str)
    {
        Debug.LogWarning(_str);
    }

    [System.Diagnostics.Conditional("ACTIVE_DEBUG")]
    public static void LogError(string _str)
    {
        Debug.LogError(_str);
    }
}
