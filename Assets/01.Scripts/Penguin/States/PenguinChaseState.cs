using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PenguinChaseState : PenguinBaseState
{
    public PenguinChaseState(Penguin penguin, PenguinStateMachine<BasicPenguinStateEnum> stateMachine, string animationBoolName)
        : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Enemy nearestEnemy = _penguin.FindNearestEnemy("Enemy");
        if (nearestEnemy != null)
            _penguin.SetTarget(nearestEnemy.transform.position);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.IsAttackRange && !_penguin.IsClickToMoving)
        {
            _stateMachine.ChangeState(BasicPenguinStateEnum.Attack);
        }

        if (_penguin.Target == null)
        {
            _stateMachine.ChangeState(BasicPenguinStateEnum.Idle);
        }

        //if (_penguin.Target != null)
        //{
        //    if (_penguin.Target.IsDead)
        //    {
        //        _stateMachine.ChangeState(BasicPenguinStateEnum.Attack);
        //    }
        //}
    }

    public override void Exit()
    {
        base.Exit();
    }

    private bool CanAttack()
    {
        return _triggerCalled;
    }
}
