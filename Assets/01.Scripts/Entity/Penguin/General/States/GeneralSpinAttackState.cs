using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralSpinAttackState : GeneralBaseState
{
    public GeneralSpinAttackState(General penguin, EntityStateMachine<GeneralPenguinStateEnum, General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
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
                _stateMachine.ChangeState(GeneralPenguinStateEnum.Chase);
                //다죽였다면 이동
                IsTargetNull(GeneralPenguinStateEnum.MustMove);
            }
        }

        else if (IsArmyCalledIn_CommandMode())
        {
            if (_penguin.WaitForCommandToArmyCalled)
            {
                _stateMachine.ChangeState(GeneralPenguinStateEnum.MustMove);
            }
        }
        else
        {
            if (_triggerCalled) //공격
            {
                _stateMachine.ChangeState(GeneralPenguinStateEnum.Chase);

                IsTargetNull(GeneralPenguinStateEnum.Idle);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
