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

        prevMode = _penguin.MyArmy.MovefocusMode;
        _penguin.MyArmy.MovefocusMode = MovefocusMode.Stop;
        _penguin.StopImmediately();

        _penguin.LookTargetImmediately();

        general.Skill.PlaySkill();
        general.Skill.IsAvaliable = false;
        AttackEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _penguin.LookTarget();

        _penguin.MyArmy.MovefocusMode = MovefocusMode.Stop;
        _penguin.StopImmediately();

        if (_triggerCalled)
        {
            _stateMachine.ChangeState(PenguinStateType.Chase);

            IsTargetNull(PenguinStateType.Idle);
        }

    }//end method


    public override void ExitState()
    {
        base.ExitState();

        _penguin.MyArmy.MovefocusMode = prevMode;
        general.Skill.IsAvaliable = true;
        //AttackExit();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        /*if (_penguin.MoveFocusMode == MovefocusMode.Command)
        {
            _stateMachine.ChangeState(PenguinStateType.Idle);
        }
        else*/
        _stateMachine.ChangeState(PenguinStateType.Impact);
    }
}
