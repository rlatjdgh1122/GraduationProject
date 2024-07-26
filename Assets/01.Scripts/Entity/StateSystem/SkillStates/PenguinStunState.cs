using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinStunState : State
{
    public PenguinStunState(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void EnterState() //한명이 때리다가 죽으면 
    {
        base.EnterState();

        AttackEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _penguin.LookTarget();



        if (_triggerCalled)
        {
            _stateMachine.ChangeState(PenguinStateType.Chase);
            //다죽였다면 이동
            IsTargetNull(PenguinStateType.MustMove);
        }



        if (_triggerCalled)
        {
            _stateMachine.ChangeState(PenguinStateType.Chase);

            if (_penguin.CurrentTarget == null)
                _stateMachine.ChangeState(PenguinStateType.Idle);
        }
    }

    public override void ExitState()
    {
        _penguin.AnimatorCompo.speed = 1;
        AttackExit();
        base.ExitState();
    }
}
