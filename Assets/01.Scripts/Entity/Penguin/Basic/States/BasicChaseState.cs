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

        if (_penguin.MoveFocusMode == MovefocusMode.Battle)
        {
            _penguin.ArmyTriggerCalled = false;
            _penguin.WaitForCommandToArmyCalled = false;
        }
        _penguin.FindFirstNearestEnemy();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        // �׳� Ŭ�� : ���󰡴� ���� ���콺 Ŭ���Ǹ� �̵�
        if (_penguin.MoveFocusMode == MovefocusMode.Command)
        {
            if (_penguin.ArmyTriggerCalled)
            {
                _stateMachine.ChangeState(BasicPenguinStateEnum.MustMove);
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
