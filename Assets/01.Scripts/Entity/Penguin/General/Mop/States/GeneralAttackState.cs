using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralAttackState : GeneralBaseState
{

    private int curAttackCount = 0;
    public GeneralAttackState(Penguin penguin, PenguinStateMachine<MopGeneralPenguinStateEnum> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {

    }

    public override void Enter()
    {

        base.Enter();
        _triggerCalled = false;
        _penguin.FindFirstNearestEnemy();
        _penguin.owner.IsMoving = false;
        _penguin.StopImmediately();
        _penguin.AnimatorCompo.speed = _penguin.attackSpeed;

        ++curAttackCount;
        if (curAttackCount % _penguin.EveryAttackCount == 0)
        {
            curAttackCount = 0;
            _stateMachine.ChangeState(MopGeneralPenguinStateEnum.AoEAttack);
        }
    }
    public override void UpdateState()
    {
        base.UpdateState();
        _penguin.LookTarget();

        if (_triggerCalled)
        {
            _stateMachine.ChangeState(MopGeneralPenguinStateEnum.Chase);

            if (_penguin.CurrentTarget == null)
                _stateMachine.ChangeState(MopGeneralPenguinStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        _penguin.AnimatorCompo.speed = 1;
        _penguin.owner.IsMoving = true;
        base.Exit();
    }

}
