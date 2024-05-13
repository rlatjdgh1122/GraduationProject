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

    protected Vector3 RaycastHit_ToCenterPos
    {
        get
        {
            Vector3 closestPointToCenter = GetClosestPointToCenter();

            if (Physics.Raycast(closestPointToCenter, (_centerPos - closestPointToCenter).normalized, out RaycastHit hit, Mathf.Infinity))
            {
                return hit.point;
            }
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

    protected virtual Vector3 GetClosestPointToCenter() // 반드시 _meshCollider의 convex를 켜줘야함
    {
        Vector3 closestPoint = _meshCollider.ClosestPoint(_centerPos);
        return closestPoint;
    }

    private void Update()
    {
        Vector3 closestPointToCenter = GetClosestPointToCenter();
        Debug.DrawRay(closestPointToCenter, (_centerPos - closestPointToCenter).normalized * 10f, Color.red, 99f);
    }

    public abstract void SetComingObejctPos(Transform parentTransform, Vector3 position);
    protected abstract void Arrived();
}
