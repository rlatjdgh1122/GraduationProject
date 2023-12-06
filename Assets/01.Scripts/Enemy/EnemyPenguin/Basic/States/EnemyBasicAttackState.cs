using UnityEngine;

public class EnemyBasicAttackState : EnemyBasicBaseState
{
    public EnemyBasicAttackState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName)
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

        if (_triggerCalled)
        {
            if (_enemy.Target != null && _enemy.IsAttackable)
            {
                _enemy.LookTarget();
                _stateMachine.ChangeState(EnemyPenguinStateEnum.Chase);
            }
            else if (_enemy.ReachedNexus)
            {
                _enemy.LookAtNexus();
            }

            if (_enemy.Target == null && !_enemy.ReachedNexus) //���� �÷��̾ ������ �ؼ����� ����
                _stateMachine.ChangeState(EnemyPenguinStateEnum.Move);

            if (_enemy.ReachedNexus)
            {
                _stateMachine.ChangeState(EnemyPenguinStateEnum.Chase);
            }

            if (!_enemy.ReachedNexus && !_enemy.IsTargetPlayerInside)
            {
                _stateMachine.ChangeState(EnemyPenguinStateEnum.Move);
            }
        }
    }

    public override void Exit()
    {
        _enemy.AnimatorCompo.speed = 1;
        base.Exit();
    }
}
