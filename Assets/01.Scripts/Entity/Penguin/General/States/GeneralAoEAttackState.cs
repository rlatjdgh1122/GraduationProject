using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralAoEAttackState : GeneralBaseState
{
    public GeneralAoEAttackState(General penguin, EntityStateMachine<GeneralPenguinStateEnum, General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
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
            _stateMachine.ChangeState(GeneralPenguinStateEnum.Chase);

            if (_penguin.CurrentTarget == null)
                _stateMachine.ChangeState(GeneralPenguinStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        _penguin.AnimatorCompo.speed = 1;
        base.Exit();
    }

}
