using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DummyPenguin : PoolableMono
{
    public float _moveDuration = 5f;

    Vector3 _initTrm;

    private void Awake()
    {
        _initTrm = GameObject.Find("InitPos").transform.position;
    }

    public void MoveToTent()
    {
        transform.DOMove(new Vector3(_initTrm.x, transform.position.y, _initTrm.z), _moveDuration)
            .OnComplete(() =>
            {
                Debug.Log("Push");

                PoolManager.Instance.Push(this);
            });
    }
}
