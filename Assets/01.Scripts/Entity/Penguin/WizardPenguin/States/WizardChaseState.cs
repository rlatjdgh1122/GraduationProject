using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardChaseState : WizardBaseState
{
    public WizardChaseState(Penguin penguin, EntityStateMachine<WizardPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
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

        // 그냥 클릭 : 따라가던 도중 마우스 클릭되면 이동
        if (IsArmyCalledIn_CommandMode())
        {
            _stateMachine.ChangeState(WizardPenguinStateEnum.MustMove);
        }
        else
        {
            if (_penguin.CurrentTarget != null)
                _penguin.MoveToCurrentTarget();

            if (_penguin.IsTargetInAttackRange)
                _stateMachine.ChangeState(WizardPenguinStateEnum.Attack);

            else IsTargetNull(WizardPenguinStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
