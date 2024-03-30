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
            {
                Debug.Log("지금 도착했다~");
                _stateMachine.ChangeState(GeneralPenguinStateEnum.Block);
            }

            else IsTargetNull(GeneralPenguinStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        _penguin.skill.OnSkillStart -= HoldShield;

        base.Exit();
    }
}
