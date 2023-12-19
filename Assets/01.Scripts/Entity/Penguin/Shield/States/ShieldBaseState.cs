public class ShieldBaseState : PenguinState<ShieldPenguinStateEnum> //상속받기 위해서 만든 짜바리 클래스
{
    public ShieldBaseState(Penguin penguin, PenguinStateMachine<ShieldPenguinStateEnum> stateMachine, string animBoolName) :
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
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Dead);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
