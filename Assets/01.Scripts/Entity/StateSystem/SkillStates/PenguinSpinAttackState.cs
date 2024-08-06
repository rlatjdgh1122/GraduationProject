using ArmySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinSpinAttackState : State
{
    General general => _penguin as General;

    public PenguinSpinAttackState(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _penguin.StopImmediately();

        _penguin.LookTargetImmediately();

        _triggerCalled = false;
        _penguin.IgnoreToArmyCalled = true;

        general.Skill.IsAvaliable = false;
        general.Skill.PlaySkill();

        AttackEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _penguin.LookTarget();

        if (_triggerCalled)
        {
            if (!_penguin.IsTargetInAttackRange)
            {
                _stateMachine.ChangeState(PenguinStateType.Chase);
            }
            else if (_penguin.IsTargetInAttackRange)
            {
                _stateMachine.ChangeState(PenguinStateType.Attack);
            }
            else IsTargetNull(PenguinStateType.Idle);
        }

    }//end method


    public override void ExitState()
    {
        if (_penguin.MyArmy.MovefocusMode == MovefocusMode.Command) //애니메이션 끝난 이후 움직이게
        {
            _penguin.MoveToClickPositon();
        }
        else if (_penguin.MyArmy.MovefocusMode == MovefocusMode.MustMove)
        {
            _penguin.MustMoveToTargetPostion();
        }

        _penguin.IgnoreToArmyCalled = false;
        general.Skill.IsAvaliable = true;

        base.ExitState();

        //AttackExit();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        //_stateMachine.ChangeState(PenguinStateType.Impact);
    }
}
