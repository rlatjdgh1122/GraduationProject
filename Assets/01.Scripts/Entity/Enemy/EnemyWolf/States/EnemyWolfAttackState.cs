using UnityEngine;

public class EnemyWolfAttackState : EnemyWolfBaseState
{
    private int _attackValue = 0;
    public EnemyWolfAttackState(Enemy enemyBase, EnemyStateMachine<EnemyWolfStateEnum> stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemy.StopImmediately();

        if(_attackValue >= _bleed.AttackEventValue)
        {
            _bleed.Bleed = true;
            _attackValue = 0;
        }
        else
        {
            _attackValue++;
        }

        _enemy.AnimatorCompo.speed = _enemy.attackSpeed;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _enemy.LookTarget();
        if (_triggerCalled) //공격이 한 차례 끝났을 때,
        {
            if (_enemy.IsTargetPlayerInside)
                _stateMachine.ChangeState(EnemyWolfStateEnum.Chase); //사거리 안에 타겟 플레이어가 있다 -> 따라가
            else
                _stateMachine.ChangeState(EnemyWolfStateEnum.Move); //없다 -> 넥서스로 Move

            _triggerCalled = false;
        }
    }

    public override void Exit()
    {
        _enemy.AnimatorCompo.speed = 1;
        base.Exit();
    }
}
