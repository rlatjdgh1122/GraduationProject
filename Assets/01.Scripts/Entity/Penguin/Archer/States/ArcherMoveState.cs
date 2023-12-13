using Polyperfect.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMoveState : ArcherBaseState
{
    public ArcherMoveState(Penguin penguin, PenguinStateMachine<ArcherPenguinStateEnum> stateMachine, string animationBoolName)
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

        if (_penguin.IsInTargetRange && !_penguin.IsClickToMoving) //타겟 안에 들어오면 && 클릭
        {
            _stateMachine.ChangeState(ArcherPenguinStateEnum.Chase);
        }

        if (_penguin.NavAgent != null && !_penguin.NavAgent.pathPending && !_penguin.NavAgent.isStopped && _penguin.NavAgent.remainingDistance < 0.1f)
        {
            if (_penguin.IsClickToMoving)
                _penguin.IsClickToMoving = false;
            _stateMachine.ChangeState(ArcherPenguinStateEnum.Idle); //목적지에 도달했을 때 Idle상태로 바꿔준다.
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
