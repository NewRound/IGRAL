using System.Collections.Generic;
using UnityEngine;

public class CoroutineRef
{
    private static Dictionary<float, WaitForSeconds> _waitForSecondsDict = new Dictionary<float, WaitForSeconds>();

    public static WaitForSeconds GetWaitForSeconds(float time)
    {
        if (!_waitForSecondsDict.ContainsKey(time))
            _waitForSecondsDict.Add(time, new WaitForSeconds(time));

        return _waitForSecondsDict[time];
    }
}
