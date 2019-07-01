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
    public static void DoWhile(this MonoBehaviour parent, float delay, Action firstMethod = null, Action secondMethod = null)
    {
        parent.StartCoroutine(DoWhile(delay, firstMethod, secondMethod));
    }

    static IEnumerator DoWhile(float delay, Action firstMethod, Action secondMethod)
    {
        float startTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup <= startTime + delay)
        {
            firstMethod?.Invoke();
            yield return null;
        }
        secondMethod?.Invoke();
    }

}
