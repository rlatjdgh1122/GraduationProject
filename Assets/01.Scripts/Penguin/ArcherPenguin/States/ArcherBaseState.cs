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
        _penguin.Input.ClickEvent += HandleClick;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.IsDead)
            _stateMachine.ChangeState(ArcherPenguinStateEnum.Dead);
    }

    private void HandleClick()
    {
        if (!_penguin.IsDead)
        {
            if (_penguin.IsInTargetRange || _penguin.IsAttackRange) _penguin.IsClickToMoving = true;
            _penguin.SetClickMovement();
            _stateMachine.ChangeState(ArcherPenguinStateEnum.Move);
        }
    }

    public override void Exit()
    {
        base.Exit();
        _penguin.Input.ClickEvent -= HandleClick;
    }
}
