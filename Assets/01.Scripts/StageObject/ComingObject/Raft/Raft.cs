using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
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
        SignalHub.OnBattlePhaseEndEvent += OnSink;
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

        for(int i = 0; i <  enemies.Length; i++)
        {
            _enemies[i].gameObject.transform.localScale *= 3;
            _enemies[i].transform.localPosition = new Vector3(0f, 2f, 0f);
        }
    }

    private void OnSink()
    {
        foreach(Enemy enemy in _enemies)
        {
            PoolManager.Instance.Push(enemy);
        }

        transform.DOMoveY(-15f, 1f).OnComplete(() =>
        {
            NavmeshManager.Instance.NavmeshBake();
            PoolManager.Instance.Push(this);
        });
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
            enemy.ColliderCompo.enabled = true;
            enemy.NavAgent.enabled = true;
            enemy.StateMachine.ChangeState(EnemyStateType.Move);
            enemy.FindNearestTarget();
        }
    }

    public void SetMoveTarget(Transform trm)
    {
        _raftMovement.SetMoveTarget(trm);


        transform.localPosition = new Vector3(transform.localPosition.x,
                                              38f,
                                              transform.localPosition.z);

    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseEndEvent -= OnSink;
    }
}
