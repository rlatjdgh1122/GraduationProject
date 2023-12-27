using System.Diagnostics;
using UnityEngine;

public class ShieldChaseState : ShieldBaseState
{
    public ShieldChaseState(Penguin penguin, PenguinStateMachine<ShieldPenguinStateEnum> stateMachine, string animationBoolName)
        : base(penguin, stateMachine, animationBoolName)
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
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Block);

        if (_penguin.CurrentTarget == null)
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Idle);

        if (_penguin.IsDead)
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Dead);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
