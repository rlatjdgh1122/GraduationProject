using System;

public class EnemyChestHitState : EnemyBaseState
{
    public EnemyChestHitState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        ChestHitEnter();
    }


    public override void UpdateState()
    {
        base.UpdateState();

        if (_triggerCalled)
        {
            //���� ��Ÿ� �ȿ� �ִ�
            if (_enemy.IsTargetInAttackRange)
                _stateMachine.ChangeState(EnemyStateType.Attack);
            else
                _stateMachine.ChangeState(EnemyStateType.Move);
        }
    }
    public override void ExitState()
    {
        base.ExitState();

        ChestHitExit();
    }
}