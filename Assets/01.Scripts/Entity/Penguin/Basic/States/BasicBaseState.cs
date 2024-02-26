public class BasicBaseState : EntityState<BasicPenguinStateEnum, Penguin> //��ӹޱ� ���ؼ� ���� ¥�ٸ� Ŭ����
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
