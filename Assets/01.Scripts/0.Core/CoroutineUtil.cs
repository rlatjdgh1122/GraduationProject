using System;
using System.Collections;
using UnityEngine;

public static class CoroutineUtil
{
    private static GameObject _coroutineObj;
    private static CoroutineExecutor _coroutineExecutor;

    static CoroutineUtil() // 생성자에서 코루틴을 실행해주기 위한 모노비헤이비어 오브젝트를 생성
    {
        _coroutineObj = new GameObject("CoroutineObj");
        _coroutineExecutor = _coroutineObj.AddComponent<CoroutineExecutor>();
    }

    public static void CallWaitForOneFrame(Action action) //1프레임 뒤에 실행
    {
        _coroutineExecutor.StartCoroutine(DoCallWaitForOneFrame(action));
    }

    public static void CallWaitForSeconds(float seconds, Action beforeAction = null, Action afterAction = null) //n초 뒤에 실행
    {
        _coroutineExecutor.StartCoroutine(DoCallWaitForSeconds(seconds, beforeAction, afterAction));
    }

    private static IEnumerator DoCallWaitForOneFrame(Action action) // 매개변수로 받은 Action을 실행해준다.
    {
        yield return null;
        action?.Invoke();
    }

    private static IEnumerator DoCallWaitForSeconds(float seconds, Action beforeAction, Action afterAction) // 매개변수로 받은 Action을 seconds초 후에 실행해준다.
    {
        beforeAction?.Invoke();
        yield return new WaitForSeconds(seconds);
        afterAction?.Invoke();
    }

    private class CoroutineExecutor : MonoBehaviour { } // 코루틴을 실행해주기 위한 모노비헤이비어오브젝트
}
