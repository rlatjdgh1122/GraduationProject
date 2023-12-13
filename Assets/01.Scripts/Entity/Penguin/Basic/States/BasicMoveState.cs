using Polyperfect.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMoveState : BasicBaseState
{
    public BasicMoveState(Penguin penguin, PenguinStateMachine<BasicPenguinStateEnum> stateMachine, string animationBoolName)
        : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
        _penguin.FindNearestEnemy("Enemy");
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.IsInTargetRange) //Ÿ�� �ȿ� ������ && Ŭ������ �ʰ� ���� �� -> Chase��
            _stateMachine.ChangeState(BasicPenguinStateEnum.Chase);

        if (_penguin.NavAgent.velocity.magnitude < 0.15f)
            _stateMachine.ChangeState(BasicPenguinStateEnum.Idle);

        //if (_penguin.NavAgent != null && !_penguin.NavAgent.pathPending && !_penguin.NavAgent.isStopped && _penguin.NavAgent.remainingDistance < 0.1f)
        //{
        //    if (_penguin.IsClickToMoving)
        //        _penguin.IsClickToMoving = false;
        //    _stateMachine.ChangeState(BasicPenguinStateEnum.Idle); //�������� �������� �� Idle���·� �ٲ��ش�.
        //}
    }

    public override void Exit()
    {
        base.Exit();
    }
}
