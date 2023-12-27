public class ShieldImpactState : ShieldBaseState
{
    public ShieldImpactState(Penguin penguin, PenguinStateMachine<ShieldPenguinStateEnum> stateMachine, string animBoolName) 
        : base(penguin, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = false;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_triggerCalled && !_penguin.IsDead)
        {
            if (_penguin.IsInnerMeleeRange)
                _stateMachine.ChangeState(ShieldPenguinStateEnum.Block);
            else
                _stateMachine.ChangeState(ShieldPenguinStateEnum.Chase);
        }
    }

    public override void Exit()
    {
        _triggerCalled = true;
        base.Exit();
    }
}
