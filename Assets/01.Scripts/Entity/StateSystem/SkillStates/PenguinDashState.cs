public class PenguinDashState : KatanaBaseState
{
    public override void EnterState()
    {
        base.EnterState();

        AttackEnter();

        _general.skill.OnSkillStart?.Invoke();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (IsArmyCalledIn_BattleMode())
        {
            if (_triggerCalled)
            {
                _stateMachine.ChangeState(PenguinStateType.Chase);
                IsTargetNull(PenguinStateType.MustMove);
            }
        }

        else if (IsArmyCalledIn_CommandMode())
        {
            if (_penguin.WaitForCommandToArmyCalled)
            {
                _stateMachine.ChangeState(PenguinStateType.MustMove);
            }
        }
        else
        {
            if (_triggerCalled) //АјАн
            {
                _stateMachine.ChangeState(PenguinStateType.Chase);

                IsTargetNull(PenguinStateType.Idle);
            }
        }
    }

    public override void ExitState()
    {
        AttackExit();

        base.ExitState();
    }
}
