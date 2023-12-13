using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DummyPenguin : PoolableMono
{
    private float _moveDuration = 4f;

    Vector3 _initTrm;
    Animator _animator;

    private void Awake()
    {
        _animator = transform.Find("Visual").GetComponent<Animator>();
        _initTrm = GameObject.Find("InitPos").transform.position;
    }

    public override void Init()
    {
    }

    public void MoveToTent()
    {
        _animator.SetBool("Idle", false);
        _animator.SetBool("Move", true);

        Vector3 targetVec = new Vector3(_initTrm.x, transform.position.y, _initTrm.z);

        transform.DOLookAt(targetVec, _moveDuration * 0.5f);

        transform.DOMove(targetVec, _moveDuration)
            .OnComplete(() =>
            {
                Debug.Log("Push");
                _animator.SetBool("Move", false);
                PoolManager.Instance.Push(this);
            });


    }
}
