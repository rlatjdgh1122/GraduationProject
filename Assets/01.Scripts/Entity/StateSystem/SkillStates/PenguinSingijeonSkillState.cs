using ArmySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PenguinSingijeonSkillState : State
{
    private General General => _penguin as General;

    public PenguinSingijeonSkillState(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        prevMode = _penguin.MyArmy.MovefocusMode;
        _penguin.MyArmy.MovefocusMode = MovefocusMode.Stop;
        _penguin.StopImmediately();

        _penguin.LookTargetImmediately();

        _triggerCalled = false;
        General.Skill.PlaySkill();
    }

    public override void UpdateState()
    {
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
    }

    public override void ExitState()
    {
        _penguin.MyArmy.MovefocusMode = prevMode;

        base.ExitState();
    }
}
