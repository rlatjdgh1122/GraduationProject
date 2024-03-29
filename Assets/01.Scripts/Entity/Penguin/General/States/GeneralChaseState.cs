using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralChaseState : GeneralBaseState
{
    public GeneralChaseState(General penguin, EntityStateMachine<GeneralPenguinStateEnum, General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        ChaseEnter();
        _penguin.StartImmediately();

        Debug.Log("벨로시티 : " + _navAgent.velocity.magnitude);
        Debug.Log("스탑 : " + _navAgent.isStopped);

        _penguin.skill.OnSkillStart += HoldShield;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (IsArmyCalledIn_CommandMode())
        {
            _stateMachine.ChangeState(GeneralPenguinStateEnum.MustMove);
        }
        else
        {
            if (_penguin.CurrentTarget != null)
                _penguin.MoveToCurrentTarget();

            if (_penguin.IsInnerMeleeRange)
                _stateMachine.ChangeState(GeneralPenguinStateEnum.Block);

            else IsTargetNull(GeneralPenguinStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        _penguin.skill.OnSkillStart -= HoldShield;

        base.Exit();
    }
}
