public class MopAoEAttackState : MopBaseState
{
    public MopAoEAttackState(Penguin penguin, EntityStateMachine<MopPenguinStateEnum, Penguin> stateMachine, string animBoolName)
        : base(penguin, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AttackEnter(); 
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _penguin.LookTarget();

        if (IsArmyCalledIn_BattleMode())
        {
            if (_triggerCalled)
            {
                if (!_penguin.IsInnerMeleeRange)
                    _stateMachine.ChangeState(MopPenguinStateEnum.Chase);

                //다죽였다면 이동
                IsTargetNull(MopPenguinStateEnum.MustMove);
            }
        }
        else if (IsArmyCalledIn_CommandMode())
        {
            if (_penguin.WaitForCommandToArmyCalled)
            {
                _stateMachine.ChangeState(MopPenguinStateEnum.MustMove);
            }
        }
        else
        {
            if (_triggerCalled) //공격
            {
                if (!_penguin.IsInnerMeleeRange)
                    _stateMachine.ChangeState(MopPenguinStateEnum.Chase);

                IsTargetNull(MopPenguinStateEnum.Idle);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
