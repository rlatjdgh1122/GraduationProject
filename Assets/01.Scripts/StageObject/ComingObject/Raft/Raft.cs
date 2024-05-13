using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raft : PoolableMono, IComingObject
{
    private Enemy[] _enemies;
    private RaftMovement _raftMovement;

    private void Awake()
    {
        _raftMovement = GetComponent<RaftMovement>();
    }

    public override void Init()
    {
        CoroutineUtil.CallWaitForSeconds(2f, null, () => _raftMovement.Move());
    }

    public void SetComingObjectInfo(Transform parentTransform, Vector3 position, ComingElements groundElements)
    {
        _raftMovement.SetComingObejctPos(parentTransform,
                                         position);
        SetEnemies(groundElements.Enemies);
    }

    public void SetEnemies(Enemy[] enemies)
    {
        _enemies = enemies;
    }

    private void OnSink()
    {
        transform.DOMoveY(-15f, 10f).OnComplete(() => PoolManager.Instance.Push(this));
    }

    public void Arrived()
    {
        ActivateEnemies();
        CoroutineUtil.CallWaitForSeconds(3f, null, () => OnSink());
    }

    private void ActivateEnemies()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.NavAgent.enabled = true;
            enemy.IsMove = true;
        }
    }

    public void SetMoveTarget(Transform trm)
    {
        _raftMovement.transform.LookAt(trm);
        _raftMovement.SetMoveTarget(trm);
    }
}
