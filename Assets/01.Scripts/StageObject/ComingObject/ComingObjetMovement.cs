using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEditor.Rendering;
using UnityEngine;

public abstract class ComingObjetMovement : MonoBehaviour
{
    [SerializeField]
    protected float _moveDuration = 5f;

    [SerializeField]
    protected LayerMask _groundLayer;

    protected MeshCollider _meshCollider;

    protected Vector3 _targetPos;
    protected Vector3 _centerPos;
    protected Vector3 _closestPointToCenter // 반드시 _meshCollider의 convex를 켜줘야함
    {
        get
        {
            Vector3 closestPoint = _meshCollider.ClosestPoint(_centerPos);
            return closestPoint;
        }
    }


    protected Vector3 RaycastHit_ToCenterPos
    {
        get
        {
            if (Physics.Raycast(_closestPointToCenter, (_centerPos - _closestPointToCenter).normalized, out RaycastHit hit, Mathf.Infinity, _groundLayer))
            {
                // 쪼만한 년 밑에 있는 큰 년 에다가 쏘기

                //Debug.Log($"{gameObject} 찾음 {hit.point}");
                //Debug.Log($"obj: {hit.collider.gameObject}");
                //Debug.Log($"root: {hit.collider.transform.root}");
                return hit.point;
            }
            else
            {
                //Debug.Log($"{_closestPointToCenter}못 찾음");
                //Debug.Log($"{_centerPos}못 찾음");
            }
            Debug.Log("안 맞았습니다.");
            return Vector3.zero;
        }
    }

    protected virtual void Awake()
    {
        _meshCollider = transform.GetChild(0).GetComponent<MeshCollider>();
    }

    public virtual void Move()
    {
        Debug.Log($"{gameObject}: {_targetPos}");
        transform.DOMove(_targetPos, _moveDuration).
            OnComplete(() =>
            {
                Arrived();
                //DOTween.To(() => _outline.OutlineColor, color => _outline.OutlineColor = color, targetColor, 0.7f).OnComplete(() =>
                //{
                //    WaveManager.Instance.OnIceArrivedEventHanlder();

                //    Debug.Log(_enemies.Length);
                //    foreach (Enemy enemy in _enemies)
                //    {
                //        enemy.IsMove = true;
                //        enemy.NavAgent.enabled = true;
                //    }
                //    SignalHub.OnBattlePhaseStartEvent -= GroundMoveHandle;
                //});
            });


    }

    //private void SetOutline()
    //{
    //    DOTween.To(() => _outline.OutlineColor, color => _outline.OutlineColor = color, endColor, 0.7f).OnComplete(() =>
    //    {
    //        _outline.enabled = false;
    //        SignalHub.OnBattlePhaseEndEvent -= GroundMove;
    //    });
    //}

    public void SetMoveTarget(Transform trm)
    {
        transform.SetParent(trm);
        _centerPos = trm.position;
    }

    private void Update()
    {
        Debug.DrawRay(_closestPointToCenter, (_centerPos - _closestPointToCenter).normalized * 100f, Color.red, 99f);
    }

    public abstract void SetComingObejctPos(Transform parentTransform, Vector3 position);
    protected abstract void Arrived();
}
