using System;
using System.Collections;
using UnityEngine;

public static class CoroutineUtil
{
    private static GameObject _coroutineObj;
    private static CoroutineExecutor _coroutineExecutor;

    static CoroutineUtil()
    {
        _coroutineObj = new GameObject("CoroutineObj");
        _coroutineExecutor = _coroutineObj.AddComponent<CoroutineExecutor>();
    }

    public static void CallWaitForOneFrame(Action action)
    {
        _coroutineExecutor.StartCoroutine(DoCallWaitForOneFrame(action));
    }

    public static void CallWaitForSeconds(float seconds, Action beforeAction = null, Action afterAction = null)
    {
        _coroutineExecutor.StartCoroutine(DoCallWaitForSeconds(seconds, beforeAction, afterAction));
    }

    private static IEnumerator DoCallWaitForOneFrame(Action action)
    {
        yield return null;
        action?.Invoke();
    }

    private static IEnumerator DoCallWaitForSeconds(float seconds, Action beforeAction, Action afterAction)
    {
        beforeAction?.Invoke();
        yield return new WaitForSeconds(seconds);
        afterAction?.Invoke();
    }

    private class CoroutineExecutor : MonoBehaviour { }
}
