public class MopAttackState : MopBaseState
{
    private int curAttackCount = 0;
    public MopAttackState(Penguin penguin, EntityStateMachine<MopPenguinStateEnum, Penguin> stateMachine, string animBoolName)
        : base(penguin, stateMachine, animBoolName)
    {
    }

    // 공격 애니메이션 loop키기
    public override void Enter() //한명이 때리다가 죽으면 
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
        _penguin.AnimatorCompo.speed = 1;
        base.Exit();
    }

    //공격이 끝날때마다
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        if (_penguin.CheckAttackEventPassive(++curAttackCount))
        {
            _penguin?.OnPassiveAttackEvent();
        }
    }
}
