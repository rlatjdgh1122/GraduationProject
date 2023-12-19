public class BasicBaseState : PenguinState<BasicPenguinStateEnum> //상속받기 위해서 만든 짜바리 클래스
{
    public BasicBaseState(Penguin penguin, PenguinStateMachine<BasicPenguinStateEnum> stateMachine, string animBoolName) :
        base(penguin, stateMachine, animBoolName)
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
