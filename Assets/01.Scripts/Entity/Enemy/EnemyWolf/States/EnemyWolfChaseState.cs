
using UnityEngine;

public class EnemyWolfChaseState : EnemyWolfBaseState
{
    public EnemyWolfChaseState(Enemy enemyBase, EnemyStateMachine<EnemyWolfStateEnum> stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_enemy.CurrentTarget != null)
            _enemy.SetTarget(_enemy.CurrentTarget.transform.position);

        if (_enemy.CanAttack)
            _stateMachine.ChangeState(EnemyWolfStateEnum.Attack); //공격 사거리 내에 들어왔다 -> Attack
        else
            _stateMachine.ChangeState(EnemyWolfStateEnum.Chase); //공격 사거리 밖이면 계속 따라가

        if (_enemy.IsProvoked)
            _stateMachine.ChangeState(EnemyWolfStateEnum.Provoked); //도발당할 시 도발State로

        if (!_enemy.IsTargetPlayerInside)
            _stateMachine.ChangeState(EnemyWolfStateEnum.Move); //플레이어 펭귄이 아예 감지 사거리를 벗어났다 -> 넥서스로 Move
    }

    public override void Exit()
    {
        base.Exit();
    }
}
