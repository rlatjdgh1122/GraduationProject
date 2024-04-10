using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGeneralChaseState : ShieldGeneralBaseState
{
    public ShieldGeneralChaseState(General penguin, EntityStateMachine<ShieldGeneralPenguinStateEnum, General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        ChaseEnter();

        _penguin.skill.OnSkillStart += HoldShield;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (IsArmyCalledIn_CommandMode())
        {
            _stateMachine.ChangeState(ShieldGeneralPenguinStateEnum.MustMove);
        }
        else
        {
            if (_penguin.CurrentTarget != null)
                _penguin.MoveToCurrentTarget();

            if (_penguin.IsInnerMeleeRange)
            {
                _stateMachine.ChangeState(ShieldGeneralPenguinStateEnum.Attack);
            }

            else IsTargetNull(ShieldGeneralPenguinStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        _penguin.skill.OnSkillStart -= HoldShield;

        base.Exit();
    }
}
