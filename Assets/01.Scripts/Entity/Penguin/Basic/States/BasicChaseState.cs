using System.Diagnostics;
using UnityEngine;

public class BasicChaseState : BasicBaseState
{
    protected Enemy nearestEnemy;

    public BasicChaseState(Penguin penguin, PenguinStateMachine<BasicPenguinStateEnum> stateMachine, string animationBoolName)
        : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
        nearestEnemy = _penguin.FindNearestEnemy("Enemy");
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (nearestEnemy != null)
            _penguin.SetTarget(nearestEnemy.transform.position);

        if (_penguin.IsAttackRange)
            _stateMachine.ChangeState(BasicPenguinStateEnum.Attack);

        if (_penguin.Target == null)
            _stateMachine.ChangeState(BasicPenguinStateEnum.Idle);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
