using UnityEngine;

public class EnemyWolfAttackState : EnemyWolfBaseState
{
    [SerializeField] private int _attackStartEventValue;

    public EnemyWolfAttackState(Enemy enemyBase, EnemyStateMachine<EnemyWolfStateEnum> stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemy.StopImmediately();
        _attackStartEventValue++;
        Debug.Log(_attackStartEventValue);
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
        _attackStartEventValue = 0;
        _enemy.AnimatorCompo.speed = 1;
        base.Exit();
    }
}
