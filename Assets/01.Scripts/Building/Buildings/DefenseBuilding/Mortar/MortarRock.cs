using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarRock : PoolableMono
{
    private float timer;

    private Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    public IEnumerator BulletMove(Vector3 startPos, Vector3 endPos)
    {
        timer = 0;
        while (transform.position.y >= 0f)
        {
            timer += Time.deltaTime;
            Vector3 tempPos = Parabola(startPos, endPos, 5, timer);
            transform.position = tempPos;
            yield return new WaitForEndOfFrame();
        }

        PoolManager.Instance.Push(this);
    }

}
