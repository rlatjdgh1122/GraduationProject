using UnityEngine;
using ArmySystem;
public class PenguinMustMoveState : State
{
    public PenguinMustMoveState(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        MustMoveEnter();
    }

    public override void UpdateState()
    {
        if (_penguin.NavAgent.remainingDistance < 0.05f)
        {
            _stateMachine.ChangeState(PenguinStateType.Idle);
        }
    }

    public override void ExitState()
    {
        base.ExitState();

        MustMoveExit();
    }
}
