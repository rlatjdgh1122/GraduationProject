//��ӹޱ� ���ؼ� ���� ¥�ٸ� Ŭ����
public class MopBaseState : PenguinState<MopPenguinStateEnum, Penguin>
{
    public MopBaseState(Penguin penguin, EntityStateMachine<MopPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        /*if (_penguin.IsDead)
            _stateMachine.ChangeState(MopPenguinStateEnum.Dead);*/
    }

    public override void Exit()
    {
        base.Exit();
    }
}
