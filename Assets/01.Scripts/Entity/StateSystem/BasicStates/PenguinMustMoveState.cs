using UnityEngine;

public class PenguinMustMoveState : State
{
    public PenguinMustMoveState(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.WaitForCommandToArmyCalled)
        {
            if (_penguin.NavAgent.remainingDistance < 0.05f)
            {
                _stateMachine.ChangeState(PenguinStateType.Idle);
            }
        }

        if (_penguin.IsInnerTargetRange
             && _penguin.MoveFocusMode == MovefocusMode.Battle)
        {
            _stateMachine.ChangeState(PenguinStateType.Chase);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
