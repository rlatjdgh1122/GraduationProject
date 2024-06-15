using ArmySystem;

public class ShieldBaseState : PenguinState<ShieldPenguinStateEnum,Penguin> //상속받기 위해서 만든 짜바리 클래스
{
    public ShieldBaseState(Penguin penguin, EntityStateMachine<ShieldPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    protected void CheckCommandModeForMovement()
    {
        if (!_penguin.ArmyTriggerCalled) return;
        if (_penguin.MovefocusMode == MovefocusMode.Command)
        {
            if (_penguin.NavAgent.velocity.magnitude > 0.05f)
                _stateMachine.ChangeState(ShieldPenguinStateEnum.Move);
        }//end command
    }

    protected void CheckCommandModeForChase()
    {
        if (!_penguin.ArmyTriggerCalled) return;

        if (_penguin.MovefocusMode == MovefocusMode.Battle)
        {
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Chase);
        }//end Battle
    }

    public override void Enter()
    {
        base.Enter();
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
