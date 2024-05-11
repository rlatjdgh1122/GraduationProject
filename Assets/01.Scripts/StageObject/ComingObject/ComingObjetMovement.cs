using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ComingObjetMovement : MonoBehaviour
{
    [SerializeField]
    protected float _moveDuration = 5f;

    [SerializeField]
    protected LayerMask _groundLayer;

    protected MeshCollider _meshCollider;

    protected Vector3 _targetPos;
    private Vector3 _centerPos;
    protected Vector3 _closestPointDirToCenter => _meshCollider.ClosestPoint(_centerPos); // 반드시 _meshCollider의 convex를 켜줘야함

    protected Vector3 RaycastHit_ToCenterPos
    {
        get
        {
            if (Physics.Raycast(_closestPointDirToCenter, (_centerPos - _closestPointDirToCenter).normalized, out RaycastHit hit, Mathf.Infinity, _groundLayer))
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

    public virtual void SetComingObejctPos(Transform parentTransform, Vector3 position)
    {
        // 중앙과 히트 포인트 사이의 거리 계산
        float centerToHitPointX = Mathf.Abs(_meshCollider.transform.position.x - RaycastHit_ToCenterPos.x);
        float centerToHitPointZ = Mathf.Abs(_meshCollider.transform.position.z - RaycastHit_ToCenterPos.z);

        // 가장 가까운 포인트와 히트 포인트 사이의 거리 계산
        float closestPointToHitPointX = Mathf.Abs(_closestPointDirToCenter.x - RaycastHit_ToCenterPos.x);
        float closestPointToHitPointZ = Mathf.Abs(_closestPointDirToCenter.z - RaycastHit_ToCenterPos.z);

        // X와 Z 거리 계산
        float xDistance = Mathf.Abs(centerToHitPointX - closestPointToHitPointX);
        float zDistance = Mathf.Abs(centerToHitPointZ - closestPointToHitPointZ);

        // 타겟 벡터 계산
        Vector3 targetVec = new Vector3(RaycastHit_ToCenterPos.x, 0f, RaycastHit_ToCenterPos.z);

        //// X 좌표에 따라 타겟 벡터 조정 (양수인지, 음수인지, 0인지)
        targetVec.x += Mathf.Sign(transform.position.x) * xDistance;
        //
        //// Z 좌표에 따라 타겟 벡터 조정 (양수인지, 음수인지, 0인지)
        targetVec.z += Mathf.Sign(transform.position.z) * zDistance;

        // 타겟 위치 설정
        _targetPos = targetVec;
    }

    protected abstract void Arrived();
}
