using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortarRock : PoolableMono
{
    private float timer;
    private DamageCaster _damageCaster;
    private MortarEffect _attackFeedback;

    private void Awake()
    {
        _damageCaster = GetComponent<DamageCaster>();
        _attackFeedback = transform.GetChild(0).GetComponent<MortarEffect>();
    }

    private Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    public IEnumerator BulletMove(Vector3 startPos, Vector3 endPos)
    {
        timer = 0;
        //여기에 거시기 그 폭발 범위 이미지
        while (transform.position.y >= -1f)
        {
            timer += Time.deltaTime;
            Vector3 tempPos = Parabola(startPos, endPos, 5, timer);
            transform.position = tempPos;
            yield return new WaitForEndOfFrame();
        }

        PoolManager.Instance.Push(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        DestroyRock();
    }

    private void DestroyRock()
    {
        _damageCaster.CastMeteorDamage(transform.position, _damageCaster.TargetLayer);
        _attackFeedback.CreateFeedback();
        SoundManager.Play3DSound(SoundName.MortarExplosion, transform.position);

        CoroutineUtil.CallWaitForSeconds(0.7f, null, () =>
        {
            _attackFeedback.FinishFeedback();
            CoroutineUtil.CallWaitForOneFrame(null);
            PoolManager.Instance.Push(this);
        });
    }
}
