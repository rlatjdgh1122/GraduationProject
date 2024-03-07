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
        if (_triggerCalled) //������ �� ���� ������ ��,
        {
            if (_enemy.IsTargetPlayerInside)
                _stateMachine.ChangeState(EnemyWolfStateEnum.Chase); //��Ÿ� �ȿ� Ÿ�� �÷��̾ �ִ� -> ����
            else
                _stateMachine.ChangeState(EnemyWolfStateEnum.Move); //���� -> �ؼ����� Move

            _triggerCalled = false;
        }
    }

    public override void Exit()
    {
        _enemy.AnimatorCompo.speed = 1;
        base.Exit();
    }
}
