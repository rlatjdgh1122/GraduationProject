using DG.Tweening;
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
            _enemies[i].transform.localPosition = new Vector3(0f, 1f, 0f);
        }
    }

    private void OnSink()
    {
        transform.DOMoveY(-15f, 10f).OnComplete(() => PoolManager.Instance.Push(this));
    }

    public void Arrived()
    {
        ActivateEnemies();
        OnSink();
        NavmeshManager.Instance.NavmeshBake();
    }

    private void ActivateEnemies()
    {
        foreach (Enemy enemy in _enemies)
        {
            enemy.NavAgent.enabled = true;
            enemy.IsMove = true;
            enemy.transform.SetParent(null);

            enemy.StateMachine.ChangeState(EnemyStateType.Move);
        }
    }

    public void SetMoveTarget(Transform trm)
    {
        _raftMovement.SetMoveTarget(trm);


        transform.localPosition = new Vector3(transform.localPosition.x,
                                              38f,
                                              transform.localPosition.z);

    }
}
