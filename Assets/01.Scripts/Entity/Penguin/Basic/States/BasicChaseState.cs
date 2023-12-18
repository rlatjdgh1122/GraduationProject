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
        _penguin.nearestEnemy = _penguin.FindNearestEnemy("Enemy");
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.nearestEnemy != null)
            _penguin.SetTarget(_penguin.nearestEnemy.transform.position);

        if (_penguin.IsAttackRange)
            _stateMachine.ChangeState(BasicPenguinStateEnum.Attack);

        if (_penguin.Target == null)
            _stateMachine.ChangeState(BasicPenguinStateEnum.Idle);

        if (_penguin.IsDead)
            _stateMachine.ChangeState(BasicPenguinStateEnum.Dead);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
