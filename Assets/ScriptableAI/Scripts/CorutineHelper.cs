using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CorutineHelper
{
    /// <summary>
    /// Execute firstMethod before time delay pass and secondMethod after.
    /// </summary>
    /// <param name="delay">time delay in seconds</param>
    /// <param name="firstMethod">Method that will be used while time delay not passed</param>
    /// <param name="secondMethod">Method that will be used after time delay passed</param>
    /// <returns></returns>
    public static IEnumerator DoWhile(this IEnumerator enumerator, float delay, Action firstMethod, Action secondMethod)
    {
        float startTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup <= startTime + delay)
        {
            firstMethod?.Invoke();
            yield return null;
        }
        secondMethod?.Invoke();
    }

    /// <summary>
    /// Execute action with delay (before / after)
    /// </summary>
    /// <param name="callback">Return true when all operations executed</param>
    /// <param name="delayBefore">Delay before action will be executed</param>
    /// <param name="delayAfter">Delay after action was executed</param>
    /// <returns></returns>
    public static IEnumerator ActWithDelay(this IEnumerator enumerator, Action action, Action<bool> callback, float delayBefore = 0, float delayAfter = 0)
    {
        if (delayBefore > 0)
            yield return new WaitForSeconds(delayBefore);

        action?.Invoke();

        if (delayAfter > 0)
            yield return new WaitForSeconds(delayAfter);

        yield return null;
        callback(true);
    }

}
