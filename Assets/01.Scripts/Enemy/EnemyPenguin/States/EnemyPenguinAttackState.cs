using UnityEngine;

public class EnemyPenguinAttackState : EnemyPenguinBaseState
{
    public EnemyPenguinAttackState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = false;
        _enemy.StopImmediately();
        _enemy.AnimatorCompo.speed = _enemy.attackSpeed;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (_enemy.ReachedNexus)
            _enemy.LookAtNexus();
        else
            _enemy.LookTarget();

        if (_triggerCalled && _enemy.Target != null)
        {
            if (!_enemy.ReachedNexus)
            {
                if (!_enemy.IsTargetPlayerInside)
                    _stateMachine.ChangeState(EnemyPenguinStateEnum.Move);
                else
                    _stateMachine.ChangeState(EnemyPenguinStateEnum.Chase);
            }
            else
            {
                if (_enemy.IsTargetPlayerInside)
                    _stateMachine.ChangeState(EnemyPenguinStateEnum.Chase);
            }
        }
    }

    public override void Exit()
    {
        _enemy.AnimatorCompo.speed = 1;
        base.Exit();
    }
}
