using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTowerAttackState : ArcherTowerBaseState
{
    public ArcherTowerAttackState(Penguin penguin, EntityStateMachine<ArcherTowerPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter() //한명이 때리다가 죽으면 
    {
        base.Enter();

        _triggerCalled = false;
        _penguin.FindFirstNearestEnemy();
        _penguin.StopImmediately();
        //_penguin.AnimatorCompo.speed = _penguin.attackSpeed;
        _penguin.AnimatorCompo.speed = 0.5f;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _penguin.LookTarget();

        if (_penguin.CurrentTarget != null)
        {
            _stateMachine.ChangeState(ArcherTowerPenguinStateEnum.Attack);
            return;
        }

        _stateMachine.ChangeState(ArcherTowerPenguinStateEnum.Idle);
    }

    public override void Exit()
    {
        _penguin.AnimatorCompo.speed = 1;
        base.Exit();
    }
}
