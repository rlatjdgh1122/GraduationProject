using UnityEngine;

public class EnemyNexusAttackState : EnemyBaseState
{
    public EnemyNexusAttackState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _enemy.HealthCompo.OnHit += ChangeTarget;

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

        if (_triggerCalled) 
        {
            if (_enemy.IsTargetInInnerRange)
                _stateMachine.ChangeState(EnemyStateType.Chase); 
        }
    }

    public override void ExitState()
    {
        base.ExitState();

        _enemy.HealthCompo.OnHit -= ChangeTarget;

        if (_enemy.UseAttackCombo)
        {
            AttackComboExit();
        }
        else
        {
            AttackExit();
        }
    }

    private void ChangeTarget()
    {
        _enemy.FindNearestTarget();
        _stateMachine.ChangeState(EnemyStateType.Chase);
    }
}
