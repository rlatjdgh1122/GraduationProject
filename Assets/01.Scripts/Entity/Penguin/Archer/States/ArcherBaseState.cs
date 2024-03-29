using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherBaseState : PenguinState<ArcherPenguinStateEnum, Penguin>
{
    public ArcherBaseState(Penguin penguin, EntityStateMachine<ArcherPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.IsDead)
            _stateMachine.ChangeState(ArcherPenguinStateEnum.Dead);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
