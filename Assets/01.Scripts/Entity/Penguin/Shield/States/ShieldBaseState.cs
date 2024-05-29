    public class ShieldBaseState : PenguinState<ShieldPenguinStateEnum,Penguin> //��ӹޱ� ���ؼ� ���� ¥�ٸ� Ŭ����
{
    public ShieldBaseState(Penguin penguin, EntityStateMachine<ShieldPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.ArmyTriggerCalled)
        {
            _penguin.StateMachine.ChangeState(PenguinStateType.MustMove);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
