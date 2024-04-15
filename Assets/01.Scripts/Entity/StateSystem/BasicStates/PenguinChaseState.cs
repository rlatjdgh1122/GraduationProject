public class PenguinChaseState : State
{
    public override void EnterState()
    {
        base.EnterState();

        ChaseEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (IsArmyCalledIn_CommandMode())
        {
            _stateMachine.ChangeState(PenguinStateType.MustMove);
        }
        else
        {
            if (_penguin.CurrentTarget != null)
                _penguin.MoveToCurrentTarget();

            if (_penguin.IsInnerMeleeRange)
            {
                _stateMachine.ChangeState(PenguinStateType.Attack);
            }

            else IsTargetNull(PenguinStateType.Idle);
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
