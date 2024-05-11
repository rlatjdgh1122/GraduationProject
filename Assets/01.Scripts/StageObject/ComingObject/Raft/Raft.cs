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
        SignalHub.OnBattlePhaseStartEvent += RaftMoveHandler;
    }

    public void SetComingObjectInfo(Transform parentTransform, Vector3 position, ComingElements groundElements)
    {
        SetEnemies(groundElements.Enemies);
    }

    private void ActivateEnemies()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.NavAgent.enabled = true;
            enemy.IsMove = true;
        }
    }

    public void SetEnemies(Enemy[] enemies)
    {
        _enemies = enemies;
    }

    private void RaftMoveHandler()
    {
        _raftMovement.Move();
    }

    private void OnSink()
    {
        transform.DOMoveY(-15f, 10f).OnComplete(() => PoolManager.Instance.Push(this));
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseStartEvent -= RaftMoveHandler;
    }
}
