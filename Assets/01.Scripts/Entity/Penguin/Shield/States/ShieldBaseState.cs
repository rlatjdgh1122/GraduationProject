public class ShieldBaseState : PenguinState<ShieldPenguinStateEnum,Penguin> //상속받기 위해서 만든 짜바리 클래스
{
    public ShieldBaseState(Penguin penguin, EntityStateMachine<ShieldPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.IsDead)
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Dead);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
