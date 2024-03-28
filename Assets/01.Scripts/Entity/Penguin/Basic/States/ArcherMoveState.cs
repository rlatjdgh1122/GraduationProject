using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMoveState : ArcherBaseState
{
    public ArcherMoveState(Penguin penguin, EntityStateMachine<ArcherPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        MoveEnter();
    }
    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.NavAgent.remainingDistance < 0.05f)
        {
            _stateMachine.ChangeState(ArcherPenguinStateEnum.Idle);
        }
        // 전투 모드 : 위치로 가던중 범위에 적이 있다면 죽이고 위치로
        if (_penguin.IsInnerTargetRange)
        {
            _stateMachine.ChangeState(ArcherPenguinStateEnum.Chase);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
