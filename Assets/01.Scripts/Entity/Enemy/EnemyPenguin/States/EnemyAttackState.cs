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

        if (_triggerCalled) //공격이 한 차례 끝났을 때,
        {

            if (_enemy.IsTargetInInnerRange)
                _stateMachine.ChangeState(EnemyStateType.Chase); //사거리 안에 타겟 플레이어가 있다 -> 따라가

            if (!_enemy.IsTargetInInnerRange) // 감지 사거리 내에서 벗어났다 -> Move (넥서스쪽으로)
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
