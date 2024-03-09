using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class MopChaseState : MopBaseState
{
    public MopChaseState(Penguin penguin, EntityStateMachine<MopPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
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
            _stateMachine.ChangeState(MopPenguinStateEnum.Attack);

        if (_penguin.CurrentTarget == null)
            _stateMachine.ChangeState(MopPenguinStateEnum.Idle);
    }

    public override void Exit()
    {
        base.Exit();
    }
}