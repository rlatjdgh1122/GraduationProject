public class PenguinDashState : KatanaBaseState
{
    DashSkill dashSkill => _general.skill as DashSkill;

    public override void EnterState()
    {
        base.EnterState();

        if (_penguin.CurrentTarget == null)
            return;

        if (_penguin.CurrentTarget != null)
            _penguin.CurrentTarget.HealthCompo.OnDied += DeadTarget;

        _triggerCalled = false;
        _penguin.WaitForCommandToArmyCalled = false;

        _penguin.AnimatorCompo.speed = _penguin.attackSpeed;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_general.canDash)
        {
            dashSkill.DashHandler();
            _general.canDash = false;
        }

        if (_triggerCalled) 
        {
            _stateMachine.ChangeState(PenguinStateType.Chase);

            IsTargetNull(PenguinStateType.Idle);
        }
    }

    public override void ExitState()
    {
        AttackExit();

        base.ExitState();
    }
}
