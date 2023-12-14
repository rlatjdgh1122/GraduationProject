using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherBaseState : PenguinState<ArcherPenguinStateEnum>
{
    public ArcherBaseState(Penguin penguin, PenguinStateMachine<ArcherPenguinStateEnum> stateMachine, string animationBoolName)
        : base(penguin, stateMachine, animationBoolName)
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
        {
            _penguin.NavAgent.enabled = false;
            _stateMachine.ChangeState(ArcherPenguinStateEnum.Dead);
        }

        if (!_penguin.enabled)
            _stateMachine.ChangeState(ArcherPenguinStateEnum.Dead);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
