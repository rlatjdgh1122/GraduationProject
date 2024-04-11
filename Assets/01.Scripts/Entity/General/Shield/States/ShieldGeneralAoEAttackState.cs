using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGeneralAoEAttackState : ShieldGeneralBaseState
{
    public ShieldGeneralAoEAttackState(General penguin, EntityStateMachine<ShieldGeneralPenguinStateEnum, General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AttackEnter();
    }
    public override void UpdateState()
    {

        base.UpdateState();

        _penguin.LookTarget();

        if (_triggerCalled)
        {
            _stateMachine.ChangeState(ShieldGeneralPenguinStateEnum.Chase);

            if (_penguin.CurrentTarget == null)
                _stateMachine.ChangeState(ShieldGeneralPenguinStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        _penguin.AnimatorCompo.speed = 1;
        base.Exit();
    }

}
