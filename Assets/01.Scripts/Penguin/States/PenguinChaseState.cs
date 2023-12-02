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
        Transform nearestEnemy = _penguin.FindNearestObjectByTag("Enemy");
        _penguin.SetTarget(nearestEnemy.position);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.AttackInable && !_penguin.IsClickToMoving)
            _stateMachine.ChangeState(BasicPenguinStateEnum.Attack);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
