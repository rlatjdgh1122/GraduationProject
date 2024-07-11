using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Parabola
{
    private static Vector3 GetParabola(Vector3 start, Vector3 end, float height, float t)
    {
        float parabolaHeight = ParabolaFunc(height, t);

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, parabolaHeight + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    private static float ParabolaFunc(float height, float t)
    {
        return -4 * height * t * t + 4 * height * t;
    }

    public static IEnumerator ParabolaMove(MonoBehaviour monoBehaviour, Vector3 startPos, Vector3 endPos, float maxTime, bool isPool, bool isRotate, Action action = null) // 화살은 풀링하면 오류나서 일단 이렇게 함
    {
        float curTime = 0;

        float distance = Vector3.Distance(startPos, endPos);
        float height = distance * 0.2f;

        while (IsActive(monoBehaviour, maxTime, curTime))
        {
            curTime += Time.deltaTime;
            Vector3 tempPos = GetParabola(startPos, endPos, height, curTime);
            monoBehaviour.transform.position = tempPos;
            if (isRotate) { monoBehaviour.transform.rotation = Quaternion.Euler(0, curTime * 720f, 0); }
            yield return new WaitForEndOfFrame();
        }

        if (isPool)
        {
            PoolManager.Instance.Push(monoBehaviour as PoolableMono);
        }
        else
        {
            monoBehaviour.gameObject.SetActive(false); // 반드시 바꿔
        }
        Debug.Log("Destroy");
        action?.Invoke();
    }

    private static bool IsActive(MonoBehaviour monoBehaviour, float maxTime, float curTime)
    {
        return curTime < maxTime && monoBehaviour.gameObject.activeInHierarchy;
    }
}
