using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalTowerPenguinWatchState : SignalTowerPenguinBaseState
{
    public SignalTowerPenguinWatchState(Penguin penguin, EntityStateMachine<SignalTowerPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter() //한명이 때리다가 죽으면 
    {
        base.Enter();

        _triggerCalled = false;
        _penguin.StopImmediately();
        _penguin.SetTarget();

        if (_penguin.CurrentTarget == null)
            _stateMachine.ChangeState(SignalTowerPenguinStateEnum.Idle);

        _penguin.AnimatorCompo.speed = 0.5f;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _penguin.LookTarget();

        if (_triggerCalled)
        {
            _stateMachine.ChangeState(SignalTowerPenguinStateEnum.Idle);
        }
        //_stateMachine.ChangeState(ArcherTowerPenguinStateEnum.Idle);
    }

    public override void Exit()
    {
        _penguin.AnimatorCompo.speed = 1;
        base.Exit();
    }
}
