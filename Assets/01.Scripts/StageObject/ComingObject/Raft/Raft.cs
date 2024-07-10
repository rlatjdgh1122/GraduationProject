using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class Raft : PoolableMono, IComingObject
{
    private List<Enemy> _enemies = new();
    private RaftMovement _raftMovement;

    private void Awake()
    {
        _raftMovement = GetComponent<RaftMovement>();
    }

    public override void Init()
    {
        CoroutineUtil.CallWaitForSeconds(2f, () => _raftMovement.Move());
        SignalHub.OnBattlePhaseEndEvent += OnSink;
        SignalHub.OnBattlePhaseEndEvent += OnBattleEnd;
    }

    public void SetComingObjectInfo(ComingElements groundElements, Transform parentTransform, Vector3 position)
    {
        _raftMovement.SetComingObejctPos(parentTransform, position);
        SetEnemies(groundElements.Enemies);
    }

    public void SetEnemies(List<Enemy> enemies)
    {
        if (_enemies.Count > 0) { _enemies.Clear(); }
        _enemies = enemies;
    }

    private void OnSink()
    {
        transform.DOMoveY(-15f, 1f).OnComplete(() =>
        {
            NavmeshManager.Instance.NavmeshBake();
            PoolManager.Instance.Push(this);
        });

        if (_enemies.Count > 0) _enemies.Clear();
    }

    public void Arrived()
    {
        NavmeshManager.Instance.NavmeshBake();
        SignalHub.OnRaftArrivedEvent?.Invoke();
        ActivateEnemies(); 
    }

    private void ActivateEnemies()
    {
        foreach (var enemy in _enemies)
        {
            enemy.IsMove = true;
            enemy.ColliderCompo.enabled = true;
            enemy.NavAgent.enabled = true;
            enemy.MouseHandlerCompo.SetColiderActive(true);
            //enemy.StateMachine.ChangeState(EnemyStateType.Move);
            enemy.FindNearestTarget();
        }
    }

    public void SetMoveTarget(Transform trm)
    {
        _raftMovement.SetMoveTarget(trm);
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseEndEvent -= OnSink;
    }

    private void OnBattleEnd()
    {
        foreach (var enemy in _enemies)
        {
            PoolManager.Instance.Push(enemy);
        }

        SignalHub.OnBattlePhaseEndEvent -= OnBattleEnd;

        if (_enemies.Count > 0) _enemies.Clear();
    }
}
