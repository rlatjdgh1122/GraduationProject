public class ShieldBaseState : PenguinState<ShieldPenguinStateEnum,Penguin> //��ӹޱ� ���ؼ� ���� ¥�ٸ� Ŭ����
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
