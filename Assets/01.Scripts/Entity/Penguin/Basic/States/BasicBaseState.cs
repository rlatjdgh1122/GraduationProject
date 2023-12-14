public class BasicBaseState : PenguinState<BasicPenguinStateEnum> //��ӹޱ� ���ؼ� ���� ¥�ٸ� Ŭ����
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
        {
            _penguin.NavAgent.enabled = false;
            _stateMachine.ChangeState(BasicPenguinStateEnum.Dead);
        }

        if (!_penguin.enabled)
            _stateMachine.ChangeState(BasicPenguinStateEnum.Dead);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
