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

    [SerializeField] // 일단 여기다가 넣음
    private int damage;

    private bool isDestoried;

    [SerializeField]
    private GameObject _mortarAttackRangeSprite;

    private Vector3 _endPos;

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
        _endPos = endPos;
        isDestoried = false;

        _mortarAttackRangeSprite.SetActive(true);
        _mortarAttackRangeSprite.transform.position = endPos;
        _mortarAttackRangeSprite.transform.SetParent(null);

        while (transform.position.y >= -1f)
        {
            timer += Time.deltaTime;
            Vector3 tempPos = Parabola(startPos, endPos, 5, timer);
            transform.position = tempPos;
            transform.rotation = Quaternion.Euler(0, timer * 720f, 0);
            yield return new WaitForEndOfFrame();
        }

        isDestoried = true;
        _mortarAttackRangeSprite.transform.SetParent(transform);
        _mortarAttackRangeSprite.SetActive(false);
        PoolManager.Instance.Push(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        DestroyRock();
    }

    private void DestroyRock()
    {
        if (!isDestoried)
        {
            isDestoried = true;
            _mortarAttackRangeSprite.transform.SetParent(transform);
            _mortarAttackRangeSprite.SetActive(false);
            _damageCaster.CastBuildingAoEDamage(transform.position, _damageCaster.TargetLayer, damage);
            _attackFeedback.CreateFeedback(_endPos);
            SoundManager.Play3DSound(SoundName.MortarExplosion, transform.position);

            CoroutineUtil.CallWaitForSeconds(0.7f, null, () =>
            {
                _attackFeedback.FinishFeedback();
                CoroutineUtil.CallWaitForOneFrame(null);
                PoolManager.Instance.Push(this);
            });
        }
    }
}
