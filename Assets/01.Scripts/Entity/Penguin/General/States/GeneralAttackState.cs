using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralAttackState : GeneralBaseState
{

    private int curAttackCount = 0;
    public GeneralAttackState(General penguin, EntityStateMachine<GeneralPenguinStateEnum, General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
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
        _penguin.AnimatorCompo.speed = 1;
        _penguin.skill.OnSkillStart -= HoldShield;
        base.Exit();
    }

}
