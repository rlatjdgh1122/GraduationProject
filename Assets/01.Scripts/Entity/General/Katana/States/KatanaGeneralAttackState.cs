using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaGeneralAttackState : KatanaGeneralBaseState
{
    public KatanaGeneralAttackState(General penguin, EntityStateMachine<KatanaGeneralStateEnum, General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
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
                if (_penguin.CurrentTarget.IsDead)
                    _stateMachine.ChangeState(KatanaGeneralStateEnum.Chase);

                IsTargetNull(KatanaGeneralStateEnum.MustMove);
            }
        }

        else if (IsArmyCalledIn_CommandMode())
        {
            if (_penguin.WaitForCommandToArmyCalled)
            {
                _stateMachine.ChangeState(KatanaGeneralStateEnum.MustMove);
            }
        }
        else
        {
            if (_triggerCalled) //АјАн
            {
                _stateMachine.ChangeState(KatanaGeneralStateEnum.Chase);

                IsTargetNull(KatanaGeneralStateEnum.Idle);
            }
        }
    }

    public override void Exit()
    {
        AttackExit();

        base.Exit();
    }
}
