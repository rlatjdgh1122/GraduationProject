using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinAttackState : PenguinBaseState
{
    public PenguinAttackState(Penguin penguin, PenguinStateMachine<BasicPenguinStateEnum> stateMachine, string animationBoolName)
        : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = false;
        _penguin.StopImmediately();
        _penguin.AnimatorCompo.speed = _penguin.attackSpeed;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _penguin.LookTarget();

        if (_triggerCalled && _penguin.Target != null)
        {
            if (_penguin.IsAttackRange)
            {
                _stateMachine.ChangeState(BasicPenguinStateEnum.Chase);
            }

            if (_penguin.Target == null) //타겟이 없다면 가만히 있음
            {
                _stateMachine.ChangeState(BasicPenguinStateEnum.Idle);
            }
        }
    }

    public override void Exit()
    {
        _penguin.AnimatorCompo.speed = 1;
        base.Exit();
    }
}
