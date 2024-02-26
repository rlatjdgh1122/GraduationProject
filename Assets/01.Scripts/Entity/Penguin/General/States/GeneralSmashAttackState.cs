using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSmashAttackState : GeneralBaseState
{
    public GeneralSmashAttackState(General penguin, EntityStateMachine<GeneralPenguinStateEnum, General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();

        _triggerCalled = false;
        _penguin.FindFirstNearestEnemy();
        _penguin.Owner.IsMoving = false;
        _penguin.StopImmediately();
        _penguin.AnimatorCompo.speed = _penguin.attackSpeed;

    }
    public override void UpdateState()
    {

        base.UpdateState();

        _penguin.LookTarget();

        if (_triggerCalled)
        {
            _stateMachine.ChangeState(GeneralPenguinStateEnum.Chase);

            if (_penguin.CurrentTarget == null)
                _stateMachine.ChangeState(GeneralPenguinStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        _penguin.AnimatorCompo.speed = 1;
        _penguin.Owner.IsMoving = true;
        base.Exit();
    }
}
