using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralChaseState : GeneralBaseState
{
    public GeneralChaseState(General penguin, EntityStateMachine<GeneralPenguinStateEnum, General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
        _penguin.FindFirstNearestEnemy();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_penguin.CurrentTarget != null)
            _penguin.SetTarget(_penguin.CurrentTarget.transform.position);

        if (_penguin.IsInnerMeleeRange)
            _stateMachine.ChangeState(GeneralPenguinStateEnum.Attack);

        if (_penguin.CurrentTarget == null)
            _stateMachine.ChangeState(GeneralPenguinStateEnum.Idle);
    }

    public override void Exit()
    {
        base.Exit();
    }


}
