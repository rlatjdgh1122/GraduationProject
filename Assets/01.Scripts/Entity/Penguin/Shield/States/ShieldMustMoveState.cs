using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArmySystem;
public class ShieldMustMoveState : ShieldBaseState
{
    public ShieldMustMoveState(Penguin penguin, EntityStateMachine<ShieldPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        MustMoveEnter();
    }
    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.NavAgent.remainingDistance < 0.05f)
        {
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        base.Exit();

        _penguin.IsMustMoving = false;
    }
}
