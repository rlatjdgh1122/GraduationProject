using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaGeneralChaseState : KatanaGeneralBaseState
{
    public KatanaGeneralChaseState(General penguin, EntityStateMachine<KatanaGeneralStateEnum, General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        ChaseEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (IsArmyCalledIn_CommandMode())
        {
            _stateMachine.ChangeState(KatanaGeneralStateEnum.MustMove);
        }
        else
        {
            if (_penguin.CurrentTarget != null)
                _penguin.MoveToCurrentTarget();

            if (_penguin.IsTargetInAttackRange)
            {
                _stateMachine.ChangeState(KatanaGeneralStateEnum.Attack);
            }

            else IsTargetNull(KatanaGeneralStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
