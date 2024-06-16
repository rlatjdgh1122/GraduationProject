public class PenguinDashState : State
{
    private General General => _penguin as General;

    public PenguinDashState(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _penguin.LookTargetImmediately();
        AttackEnter();

        if (_penguin.CurrentTarget == null)
        {
            _stateMachine.ChangeState(PenguinStateType.Idle);
        }    
        else if (_penguin.IsTargetInThrowRange)
        {
            _triggerCalled = false;
            General.Skill.PlaySkill();
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_triggerCalled) 
        {
            if (General.Skill.CanUseSkill)
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
