using UnityEngine;

public class EnemyArcherAttackState : EnemyBasicBaseState
{
    public EnemyArcherAttackState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName)
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

            //if (_enemy.Target == null && !_enemy.ReachedNexus) //만약 플레이어가 없으면 넥서스로 돌격
            //    _stateMachine.ChangeState(EnemyPenguinStateEnum.Move);

            if (_enemy.ReachedNexus)
            {
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
