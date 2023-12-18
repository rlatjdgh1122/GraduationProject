public class ShieldDeadState : ShieldBaseState
{
    public ShieldDeadState(Penguin penguin, PenguinStateMachine<ShieldPenguinStateEnum> stateMachine, string animBoolName)
        : base(penguin, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _penguin.CurrentTarget.FindNearestPenguin("Player");
        _penguin.CurrentTarget = null;
        _penguin.tag = "Untagged";
        _penguin.CharController.enabled = false;
        _penguin.NavAgent.enabled = false;
        //_penguin.enabled = false;
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
