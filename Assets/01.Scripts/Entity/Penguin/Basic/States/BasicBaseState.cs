public class BasicBaseState : EntityState<BasicPenguinStateEnum, Penguin> //상속받기 위해서 만든 짜바리 클래스
{
    public BasicBaseState(Penguin penguin, EntityStateMachine<BasicPenguinStateEnum,Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.IsDead)
            _stateMachine.ChangeState(BasicPenguinStateEnum.Dead);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
