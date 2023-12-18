using System.Diagnostics;
using UnityEngine;

public class BasicChaseState : BasicBaseState
{
    public BasicChaseState(Penguin penguin, PenguinStateMachine<BasicPenguinStateEnum> stateMachine, string animationBoolName)
        : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
        _penguin.nearestEnemy = _penguin.FindNearestEnemy(_penguin.maxDetectedCount);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.nearestEnemy != null)
            _penguin.SetTarget(_penguin.CurrentTarget.transform.position);

        if (_penguin.IsInnerMeleeRange)
            _stateMachine.ChangeState(BasicPenguinStateEnum.Attack);

        if (_penguin.CurrentTarget == null)
            _stateMachine.ChangeState(BasicPenguinStateEnum.Idle);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
