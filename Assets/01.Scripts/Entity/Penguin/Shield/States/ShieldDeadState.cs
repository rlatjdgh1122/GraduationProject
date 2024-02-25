public class ShieldDeadState : ShieldBaseState
{
    public ShieldDeadState(Penguin penguin, PenguinStateMachine<ShieldPenguinStateEnum> stateMachine, string animBoolName)
        : base(penguin, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        _triggerCalled = true;
        _penguin.CurrentTarget = null;
        //_penguin.CharController.enabled = false;
        _penguin.NavAgent.enabled = false;
        _penguin.enabled = false;

        foreach (var e in _penguin.FindNearestEnemy(5)) //�ϴ� �ӽ÷� 5�������� �̰͵� SO�� ������
        {
            if (!e.IsDead)
                e.IsProvoked = false;
        }
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
