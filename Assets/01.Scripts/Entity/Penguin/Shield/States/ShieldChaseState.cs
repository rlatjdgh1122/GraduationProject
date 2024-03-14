using System.Diagnostics;
using UnityEngine;

public class ShieldChaseState : ShieldBaseState
{
    public ShieldChaseState(Penguin penguin, EntityStateMachine<ShieldPenguinStateEnum, Penguin> stateMachine, string animationBoolName)
        : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;

        ChaseEnter();

        _penguin.FindFirstNearestEnemy();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (IsArmyCalledIn_CommandMode())
        {
            _stateMachine.ChangeState(ShieldPenguinStateEnum.MustMove);
        }

        if (_penguin.CurrentTarget != null)
            _penguin.MoveToCurrentTarget();

        if (_penguin.IsInnerMeleeRange)
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Block);

        IsTargetNull(ShieldPenguinStateEnum.Idle);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
