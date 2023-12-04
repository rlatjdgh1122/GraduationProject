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
        _penguin.StopImmediately();
        _penguin.AnimatorCompo.speed = _penguin.attackSpeed;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _penguin.LookTarget();

        if (_penguin.Target == null)
        {
            _stateMachine.ChangeState(BasicPenguinStateEnum.Idle);
        }

        if (!_penguin.IsAttackRange && _penguin.Target != null)
        {
            _stateMachine.ChangeState(BasicPenguinStateEnum.Chase);
        }
    }

    public override void Exit()
    {
        _penguin.AnimatorCompo.speed = 1;
        base.Exit();
    }
}
