
using UnityEngine;

public class EnemyPenguinChaseState : EnemyPenguinBaseState
{
    public EnemyPenguinChaseState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;

        _enemy.FindNearestTarget();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_enemy.CurrentTarget != null)
            _enemy.MoveToCurrentTarget();

        if (_enemy.CanAttack)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Attack); //공격 사거리 내에 들어왔다 -> Attack
        else
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Chase); //공격 사거리 밖이면 계속 따라가

        if (_enemy.IsProvoked)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Provoked); //도발당할 시 도발State로

        if (!_enemy.IsTargetPlayerInside)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Move); //플레이어 펭귄이 아예 감지 사거리를 벗어났다 -> 넥서스로 Move
    }

    public override void Exit()
    {
        base.Exit();
    }
}
