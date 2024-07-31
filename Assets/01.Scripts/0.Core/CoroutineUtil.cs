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

    public static void CallWaitForSeconds(float seconds, Action afterAction) //n초 뒤에 실행
    {
        _coroutineExecutor.StartCoroutine(DoCallWaitForSeconds(seconds, afterAction));
    }

    //action이 true가 되면 실행
    public static IEnumerator CallWaitForAction(Func<bool> predicate, Action afterAction = null) 
    {
        yield return new WaitUntil(predicate);
        afterAction?.Invoke();
        Debug.Log("이거 잘되는군");
    }

    //predicate가 true가 될때동안 action을 반복 수행해주고 주기는 heartBeat
    public static void CallWaitForActionUntilTrue(bool predicate, Action action, float heartBeat = 0.02f, Action afterAction = null)
    {
        _coroutineExecutor.StartCoroutine(DoCallWaitForActionUntilTrue(heartBeat, predicate, action, afterAction));
    }

    private static IEnumerator DoCallWaitForOneFrame(Action action) // 매개변수로 받은 Action을 실행해준다.
    {
        yield return null;
        action?.Invoke();
    }

    private static IEnumerator DoCallWaitForSeconds(float seconds, Action afterAction) // 매개변수로 받은 Action을 seconds초 후에 실행해준다.
    {
        yield return new WaitForSeconds(seconds);
        afterAction?.Invoke();
    }

    private static IEnumerator DoCallWaitForActionUntilTrue(float heartBeat, bool predicate, Action action, Action afterAction)
    {
        while (predicate)
        {
            yield return new WaitForSeconds(heartBeat);
            action?.Invoke();
        }
        afterAction?.Invoke();
    }





    private class CoroutineExecutor : MonoBehaviour { } // 코루틴을 실행해주기 위한 모노비헤이비어오브젝트
}
