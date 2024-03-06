using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class BasicChaseState : BasicBaseState
{
    public BasicChaseState(Penguin penguin, EntityStateMachine<BasicPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
        _penguin.ArmyTriggerCalled = false;

        _penguin.FindFirstNearestEnemy();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        //따라가던 도중 마우스 클릭되면 이동
        if (_penguin.ArmyTriggerCalled)
        {
            _penguin.SetTarget(_penguin.GetSeatPosition());
            _stateMachine.ChangeState(BasicPenguinStateEnum.Move);
        }
        else
        {
            if (_penguin.CurrentTarget != null)
                _penguin.SetTarget(_penguin.CurrentTarget.transform.position);

            if (_penguin.IsInnerMeleeRange)
                _stateMachine.ChangeState(BasicPenguinStateEnum.Attack);

            if (_penguin.CurrentTarget == null)
                _stateMachine.ChangeState(BasicPenguinStateEnum.Idle);
        }

    }

    public override void Exit()
    {
        base.Exit();
    }
}
