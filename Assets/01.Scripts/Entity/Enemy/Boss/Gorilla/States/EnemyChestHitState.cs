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
            //공격 사거리 안에 있다
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