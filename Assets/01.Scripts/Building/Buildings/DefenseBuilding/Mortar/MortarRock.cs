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

    [SerializeField] // �ϴ� ����ٰ� ����
    private int damage;

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
        //���⿡ �Žñ� �� ���� ���� �̹���
        while (transform.position.y >= -1f)
        {
            timer += Time.deltaTime;
            Vector3 tempPos = Parabola(startPos, endPos, 5, timer);
            transform.position = tempPos;
            transform.rotation = Quaternion.Euler(0, timer * 720f, 0);
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
        _damageCaster.CastBuildingAoEDamage(transform.position, _damageCaster.TargetLayer, damage);
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
