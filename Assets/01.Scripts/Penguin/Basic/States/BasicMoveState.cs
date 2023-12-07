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

        if (_penguin.IsInTargetRange && !_penguin.IsClickToMoving) //Ÿ�� �ȿ� ������ && Ŭ��
        {
            _stateMachine.ChangeState(BasicPenguinStateEnum.Chase);
        }

        if (_penguin.Target != null)
        {
            if (_penguin.NavAgent.remainingDistance < 0.1f && !_penguin.NavAgent.pathPending)
            {
                if (_penguin.IsClickToMoving)
                    _penguin.IsClickToMoving = false;
                _stateMachine.ChangeState(BasicPenguinStateEnum.Idle); //�������� �������� �� Idle���·� �ٲ��ش�.
            }
        }    
    }

    public override void Exit()
    {
        base.Exit();
    }
}
