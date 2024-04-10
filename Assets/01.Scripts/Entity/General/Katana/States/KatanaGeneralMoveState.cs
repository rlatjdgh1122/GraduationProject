using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaGeneralMoveState : KatanaGeneralBaseState
{
    public KatanaGeneralMoveState(General penguin, EntityStateMachine<KatanaGeneralStateEnum, General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.NavAgent.velocity.magnitude < 0.05f)
            _stateMachine.ChangeState(KatanaGeneralStateEnum.Idle);

        if (_penguin.IsInnerTargetRange
            && _penguin.MoveFocusMode == MovefocusMode.Battle)
            _stateMachine.ChangeState(KatanaGeneralStateEnum.Chase);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
