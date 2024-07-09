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

        if (_triggerCalled) //공격 이후
        {
            _stateMachine.ChangeState(EnemyStateType.LookAt);
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

        _entityActionData.AddAttackCount();

        //이거 안쓸수도
        if (_enemy.CheckAttackEventPassive(++curAttackCount))
        {
            _enemy.OnPassiveAttackEvent();

            curAttackCount = 0;
        }
    }
}
