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
        _penguin.FindNearestEnemy("Enemy");
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.IsAttackRange && !_penguin.IsClickToMoving)
            _stateMachine.ChangeState(ArcherPenguinStateEnum.Attack);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
