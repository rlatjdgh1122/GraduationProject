public class MopAttackState : MopBaseState
{
    private int curAttackCount = 0;
    public MopAttackState(Penguin penguin, EntityStateMachine<MopPenguinStateEnum, Penguin> stateMachine, string animBoolName)
        : base(penguin, stateMachine, animBoolName)
    {
    }

    // ���� �ִϸ��̼� loopŰ��
    public override void Enter() //�Ѹ��� �����ٰ� ������ 
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

                //���׿��ٸ� �̵�
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
            if (_triggerCalled) //����
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

    //������ ����������
    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        if (_penguin.CheckAttackEventPassive(++curAttackCount))
        {
            _penguin?.OnPassiveAttackEvent();
        }
    }
}
