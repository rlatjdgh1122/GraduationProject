public class PenguinDashState : KatanaBaseState
{
    DashSkill dashSkill => _general.skill as DashSkill;

    public override void EnterState()
    {
        base.EnterState();

        _penguin.LookTargetImmediately();

        if (_penguin.CurrentTarget == null)
        {
            _stateMachine.ChangeState(PenguinStateType.Idle);
        }    
        else
        {
            _triggerCalled = false;
            dashSkill.DashHandler();
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_triggerCalled) 
        {
            if (dashSkill.canDash)
            {
                _stateMachine.ChangeState(PenguinStateType.Dash);
            }
            else
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
