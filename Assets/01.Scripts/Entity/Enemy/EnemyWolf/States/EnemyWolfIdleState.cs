
using UnityEngine;

public class EnemyWolfIdleState : EnemyWolfBaseState
{
    public EnemyWolfIdleState(Enemy enemyBase, EnemyStateMachine<EnemyWolfStateEnum> stateMachine, string animBoolName)
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

        if (_enemy.IsMove)
            _stateMachine.ChangeState(EnemyWolfStateEnum.Move); //IsMove �� ������ True�̸� �ؼ����� Move

        //if (_enemy.IsTargetPlayerInside)
        //{
        //    Debug.Log("ChaseState_Wolf");
        //    _stateMachine.ChangeState(EnemyWolfStateEnum.Chase); //���� ��Ÿ� �ȿ� Ÿ�� �÷��̾ �ִ� -> �Ѿư�
        //}
    }

    public override void Exit()
    {
        base.Exit();
    }
}
