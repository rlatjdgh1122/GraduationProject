using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RaftMovement : ComingObjetMovement
{
    private Raft _raft;

    protected override void Awake()
    {
        base.Awake();

        _raft = GetComponent<Raft>(); // 아 몰라 시간 업음
    }

    protected override void Arrived()
    {
        NavmeshManager.Instance.NavmeshBake();
        //CoroutineUtil.CallWaitForOneFrame(() => _raft.Arrived());
        CoroutineUtil.CallWaitForSeconds(1f, () => _raft.Arrived());
    }

    protected override Vector3 GetClosestPointToCenter()
    {
        Vector3 closestPoint = _meshCollider.ClosestPoint(_centerPos);
        closestPoint.y = _centerPos.y;
        return closestPoint;
    }
}
