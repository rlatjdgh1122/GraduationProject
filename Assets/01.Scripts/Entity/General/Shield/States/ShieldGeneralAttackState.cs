using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGeneralAttackState : ShieldGeneralBaseState
{
    public ShieldGeneralAttackState(General penguin, EntityStateMachine<ShieldGeneralPenguinStateEnum, General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        _penguin.skill.OnSkillStart += HoldShield;

        AttackEnter();
    }
    public override void UpdateState()
    {
        base.UpdateState();

        _penguin.LookTarget();

        if (IsArmyCalledIn_BattleMode())
        {
            if (_triggerCalled)
            {
                if (_penguin.CurrentTarget.IsDead)
                    _stateMachine.ChangeState(ShieldGeneralPenguinStateEnum.Chase);

                //다죽였다면 이동
                IsTargetNull(ShieldGeneralPenguinStateEnum.MustMove);
            }
        }

        else if (IsArmyCalledIn_CommandMode())
        {
            if (_penguin.WaitForCommandToArmyCalled)
            {
                _stateMachine.ChangeState(ShieldGeneralPenguinStateEnum.MustMove);
            }
        }
        else
        {
            if (_triggerCalled) //공격
            {
                _stateMachine.ChangeState(ShieldGeneralPenguinStateEnum.Chase);

                IsTargetNull(ShieldGeneralPenguinStateEnum.Idle);
            }
        }
    }

    public override void Exit()
    {
        _penguin.skill.OnSkillStart -= HoldShield;
        base.Exit();
    }
}