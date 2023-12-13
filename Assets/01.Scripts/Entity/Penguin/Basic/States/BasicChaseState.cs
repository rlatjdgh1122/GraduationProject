public class BasicChaseState : BasicBaseState
{
    public BasicChaseState(Penguin penguin, PenguinStateMachine<BasicPenguinStateEnum> stateMachine, string animationBoolName)
        : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
        Enemy nearestEnemy = _penguin.FindNearestEnemy("Enemy");
        if (nearestEnemy != null)
            _penguin.SetTarget(nearestEnemy.transform.position);
            //ArmySystem.Instace.SetArmyMovePostiton(nearestEnemy.transform.position, _penguin.owner.Legion);
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.IsAttackRange)
        {
            _stateMachine.ChangeState(BasicPenguinStateEnum.Attack);
        }

        if (_penguin.Target == null)
        {
            _stateMachine.ChangeState(BasicPenguinStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
