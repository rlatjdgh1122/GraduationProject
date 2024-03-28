using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherIdleState : ArcherBaseState
{
    public ArcherIdleState(Penguin penguin, EntityStateMachine<ArcherPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        IdleEnter();
    }
    public override void UpdateState()
    {
        base.UpdateState();

        //적사거리가 들어오고 /*-군사들이 위치로 이동했다면-*/ 주석
        if (_penguin.NavAgent.velocity.magnitude > 0.05f)
            _stateMachine.ChangeState(ArcherPenguinStateEnum.Move);
        else if (_penguin.IsInnerTargetRange)
            _stateMachine.ChangeState(ArcherPenguinStateEnum.Chase);
    }


    public override void Exit()
    {
        IdleExit();

        base.Exit();
    }
}
