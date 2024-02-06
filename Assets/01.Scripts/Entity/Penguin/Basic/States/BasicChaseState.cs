using System.Diagnostics;
using System.Linq;
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
        _penguin.FindFirstNearestEnemy();
    }

    public override void UpdateState()  
    {
        base.UpdateState();

        if (_penguin.CurrentTarget != null)
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
