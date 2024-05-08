public class EnemyAttackState : EnemyBaseState
{
    private int curAttackCount = 0;

    public EnemyAttackState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        if (_enemy.UseAttackCombo)
        {
            AttackComboEnter();
        }
        else
        {
            AttackEnter();
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _enemy.LookTarget();

        if (_triggerCalled) //������ �� ���� ������ ��,
        {

            if (_enemy.IsTargetInInnerRange)
                _stateMachine.ChangeState(EnemyStateType.Chase); //��Ÿ� �ȿ� Ÿ�� �÷��̾ �ִ� -> ����

            if (!_enemy.IsTargetInInnerRange) // ���� ��Ÿ� ������ ����� -> Move (�ؼ���������)
                _stateMachine.ChangeState(EnemyStateType.Move);
        }

    }

    public override void ExitState()
    {
        base.ExitState();

        if (_enemy.UseAttackCombo)
        {
            AttackComboExit();
        }
        else
        {
            AttackExit();
        }
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        if (_enemy.CheckAttackEventPassive(++curAttackCount))
        {
            _enemy.OnPassiveAttackEvent();

            curAttackCount = 0;
        }
    }
}
