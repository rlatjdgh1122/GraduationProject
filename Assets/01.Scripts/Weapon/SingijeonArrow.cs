using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class SingijeonArrow : Arrow
{
    public override void Setting(TargetObject owner, LayerMask layer)
    {
        base.Setting(owner, layer);
    }

    public override void Fire(Vector3 endPos)
    {
        StartCoroutine(ArrowMove(transform.position, endPos));
    }

    private IEnumerator ArrowMove(Vector3 startPos, Vector3 endPos)
    {
        float timer = 0;
        bool isDestoried = false;

        float distance = Vector3.Distance(startPos, endPos);

        float height = distance * 0.2f;

        while (!isDestoried)
        {
            if (transform.position.y <= -3f) // -1보다 작아지면 바다에 빠진 것
            {
                break;
            }
            timer += Time.deltaTime;
            Vector3 tempPos = Parabola.GetParabola(startPos, endPos, height, timer);
            transform.position = tempPos;
            yield return new WaitForEndOfFrame();
        }

        if (!isDestoried)
        {
            isDestoried = true;
            PoolManager.Instance.Push(this);
        }

        yield return null;
    }
}
