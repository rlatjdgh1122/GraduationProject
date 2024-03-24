using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralAttackState : GeneralBaseState
{

    private int curAttackCount = 0;
    public GeneralAttackState(General penguin, EntityStateMachine<GeneralPenguinStateEnum,General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        _penguin.skill.OnSkillCompleted += HoldShield;

        AttackEnter();
    }
    public override void UpdateState()
    {
        base.UpdateState();

        _penguin.LookTarget();

        if (_triggerCalled)
        {
            _stateMachine.ChangeState(GeneralPenguinStateEnum.Chase);

            if (_penguin.CurrentTarget == null)
                _stateMachine.ChangeState(GeneralPenguinStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        _penguin.AnimatorCompo.speed = 1;
        _penguin.skill.OnSkillCompleted -= HoldShield;
        base.Exit();
    }

}
