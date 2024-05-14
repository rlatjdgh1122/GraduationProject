public class EnemyBombPenguinAttackState : EnemyBaseState
{
    private int curAttackCount = 0;

    public EnemyBombPenguinAttackState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {

    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _enemy.LookTarget();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();
    }
}
