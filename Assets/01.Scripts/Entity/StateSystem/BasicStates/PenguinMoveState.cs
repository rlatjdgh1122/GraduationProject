using ArmySystem;

public class PenguinMoveState : State

{
    public PenguinMoveState(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        MoveEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.NavAgent.velocity.magnitude < 0.05f)
            _stateMachine.ChangeState(PenguinStateType.Idle);

        /*if (_penguin.MoveFocusMode == MovefocusMode.Battle)*/
        {
            if (_penguin.IsTargetInInnerRange)
                _stateMachine.ChangeState(PenguinStateType.Chase);
        }
    }

    public override void ExitState()
    {
        base.ExitState();

        MoveExit();
    }
}