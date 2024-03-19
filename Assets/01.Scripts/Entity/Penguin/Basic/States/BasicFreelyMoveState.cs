using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BasicFreelyMoveState : BasicBaseState
{
    private float time = 0.0f;
    private float randomMoveTime;
    private float normalNavSpeed;

    public BasicFreelyMoveState(Penguin penguin, EntityStateMachine<BasicPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
        time = 0.0f;

        normalNavSpeed = _penguin.NavAgent.speed;
        _penguin.NavAgent.speed = 1.0f;

        randomMoveTime = Random.Range(1.0f, 3.0f);
        _penguin.MoveToPosition(GetRandomPoint());
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (WaveManager.Instance.IsBattlePhase)
        {
            _penguin.NavAgent.speed = normalNavSpeed;
            _penguin.SetFreelyMoveAble(false);
            //_stateMachine.ChangeState(BasicPenguinStateEnum.Move);
        }

        time += Time.deltaTime;
        if(time >= randomMoveTime ||
           _penguin.NavAgent.velocity.magnitude < 0.05f)
        {
            _stateMachine.ChangeState(BasicPenguinStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        base.Exit();

        _penguin.NavAgent.speed = normalNavSpeed;
    }

    private Vector3 GetRandomPoint()
    {
        Vector3 randomPos = Random.insideUnitSphere * 10.0f + _penguin.transform.position;

        NavMeshHit hit;

        NavMesh.SamplePosition(randomPos, out hit, 10.0f, NavMesh.AllAreas);
        return hit.position; // NavMesh ���� ���� ��ġ�� ��ȯ
    }
}
