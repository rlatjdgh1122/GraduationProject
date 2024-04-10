using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGeneralSpinAttackState : ShieldGeneralBaseState
{
    public ShieldGeneralSpinAttackState(General penguin, EntityStateMachine<ShieldGeneralPenguinStateEnum, General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
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

        if (IsArmyCalledIn_BattleMode())
        {
            if (_triggerCalled)
            {
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
        base.Exit();
    }
}
