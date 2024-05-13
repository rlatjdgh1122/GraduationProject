using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RaftMovement : ComingObjetMovement
{
    private Raft _raft;

    private OffMeshLink _offMeshLink;

    protected override void Awake()
    {
        base.Awake();

        _raft = GetComponent<Raft>(); // 아 몰라 시간 업음
        _offMeshLink = GetComponent<OffMeshLink>();
    }

    protected override void Arrived()
    {
        Debug.Log("가라앉기");
        _raft.Arrived();


        GameObject temp = new GameObject();
        temp.transform.position = RaycastHit_ToCenterPos;

        GameObject temp2 = new GameObject();
        temp2.transform.position = GetClosestPointToCenter();

        _offMeshLink.endTransform = temp.transform;
        _offMeshLink.startTransform = temp2.transform;
    }

    public override void SetComingObejctPos(Transform parentTransform, Vector3 position)
    {
        // 중앙과 히트 포인트 사이의 거리 계산
        float centerToHitPointX = Mathf.Abs(_meshCollider.transform.position.x - RaycastHit_ToCenterPos.x);
        float centerToHitPointZ = Mathf.Abs(_meshCollider.transform.position.z - RaycastHit_ToCenterPos.z);

        Vector3 closestPointToCenter = GetClosestPointToCenter();

        // 가장 가까운 포인트와 히트 포인트 사이의 거리 계산
        float closestPointToHitPointX = Mathf.Abs(closestPointToCenter.x - RaycastHit_ToCenterPos.x);
        float closestPointToHitPointZ = Mathf.Abs(closestPointToCenter.z - RaycastHit_ToCenterPos.z);

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

    protected override Vector3 GetClosestPointToCenter()
    {
        Vector3 closestPoint = _meshCollider.ClosestPoint(_centerPos);
        closestPoint.y = _centerPos.y;
        return closestPoint;
    }
}
