public class MopAoEAttackState : MopBaseState
{
    public MopAoEAttackState(Penguin penguin, EntityStateMachine<MopPenguinStateEnum, Penguin> stateMachine, string animBoolName)
        : base(penguin, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = false;
        _penguin.WaitForCommandToArmyCalled = false;
        _penguin.FindFirstNearestEnemy();
        _penguin.StopImmediately();
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
        if (IsArmyCalledIn_BattleMode())
        {
            _stateMachine.ChangeState(MopPenguinStateEnum.Chase);
            //���׿��ٸ� �̵�
            IsTargetNull(MopPenguinStateEnum.MustMove);
        }

        if (IsArmyCalledIn_CommandMode())
        {
            if (_penguin.WaitForCommandToArmyCalled)
            {
                _stateMachine.ChangeState(MopPenguinStateEnum.MustMove);
            }
        }
        else
        {
            if (_triggerCalled) //����
            {
                _stateMachine.ChangeState(MopPenguinStateEnum.Chase);

                IsTargetNull(MopPenguinStateEnum.Idle);
            }
        }

    }
    public override void UpdateState()
    {
        base.UpdateState();

        _penguin.LookTarget();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
