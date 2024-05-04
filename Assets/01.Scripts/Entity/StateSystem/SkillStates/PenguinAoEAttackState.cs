using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinAoEAttackState : State
{
    public PenguinAoEAttackState(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
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
                if (!_penguin.IsTargetInAttackRange)
                    _stateMachine.ChangeState(PenguinStateType.Chase);

                //다죽였다면 이동
                IsTargetNull(PenguinStateType.MustMove);
            }
        }
        else if (IsArmyCalledIn_CommandMode())
        {
            if (_penguin.WaitForCommandToArmyCalled)
            {
                _stateMachine.ChangeState(PenguinStateType.MustMove);
            }
        }
        else
        {
            if (_triggerCalled) //공격
            {
                if (!_penguin.IsTargetInAttackRange)
                    _stateMachine.ChangeState(PenguinStateType.Chase);

                IsTargetNull(PenguinStateType.Idle);
            }
        }
    }
    public override void ExitState()
    {
        base.ExitState();
    }
}
