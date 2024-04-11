using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAttackState : WizardBaseState
{
    public WizardAttackState(Penguin penguin, EntityStateMachine<WizardPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
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

        if (IsArmyCalledIn_BattleMode())
        {
            if (_triggerCalled)
            {
                if (!_penguin.IsInnerMeleeRange)
                    _stateMachine.ChangeState(WizardPenguinStateEnum.Chase);

                //���׿��ٸ� �̵�
                IsTargetNull(WizardPenguinStateEnum.MustMove);
            }
        }
        else if (IsArmyCalledIn_CommandMode())
        {
            if (_penguin.WaitForCommandToArmyCalled)
            {
                _stateMachine.ChangeState(WizardPenguinStateEnum.MustMove);
            }
        }
        else
        {
            if (_triggerCalled) //����
            {
                if (!_penguin.IsInnerMeleeRange)
                    _stateMachine.ChangeState(WizardPenguinStateEnum.Chase);

                IsTargetNull(WizardPenguinStateEnum.Idle);
            }
        }
    }

    public override void Exit()
    {
        _penguin.AnimatorCompo.speed = 1;

        AttackExit();
        base.Exit();
    }
}
