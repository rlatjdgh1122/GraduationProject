using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherIdleState : ArcherBaseState
{
    public ArcherIdleState(Penguin penguin, PenguinStateMachine<ArcherPenguinStateEnum> stateMachine, string animationBoolName) 
        : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.IsInTargetRange && !_penguin.IsClickToMoving)
            _stateMachine.ChangeState(ArcherPenguinStateEnum.Attack);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
