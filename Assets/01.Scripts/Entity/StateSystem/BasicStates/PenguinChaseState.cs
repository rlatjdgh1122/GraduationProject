using ArmySystem;

public class PenguinChaseState : State
{
    public PenguinChaseState(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        ChaseEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        //무조건 현재 적한테 이동
        if (_penguin.CurrentTarget != null)
            _penguin.MoveToCurrentTarget();


        if (_penguin.IsTargetInAttackRange)
        {
            _stateMachine.ChangeState(PenguinStateType.Attack);
        }

        else IsTargetNull(PenguinStateType.Idle);

        CheckCommandModeForMovement();
    }

    public override void ExitState()
    {
        base.ExitState();

        ChaseExit();
    }
}