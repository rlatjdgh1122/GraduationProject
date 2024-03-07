using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTowerAttackState : ArcherTowerBaseState
{
    public ArcherTowerAttackState(Penguin penguin, EntityStateMachine<ArcherTowerPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter() //�Ѹ��� �����ٰ� ������ 
    {
        base.Enter();
        _triggerCalled = false;
        _penguin.FindFirstNearestEnemy();
        _penguin.Owner.IsMoving = false;
        _penguin.StopImmediately();
        _penguin.AnimatorCompo.speed = _penguin.attackSpeed;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _penguin.LookTarget();

        if (_triggerCalled)
        {
            if (_penguin.CurrentTarget == null)
                _stateMachine.ChangeState(ArcherTowerPenguinStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        _penguin.AnimatorCompo.speed = 1;
        _penguin.Owner.IsMoving = true;
        base.Exit();
    }
}
