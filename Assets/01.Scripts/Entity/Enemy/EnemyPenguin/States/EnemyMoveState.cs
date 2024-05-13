using UnityEngine;

public class EnemyMoveState : EnemyBaseState
{
    public EnemyMoveState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }
    public override void EnterState()
    {
        base.EnterState();

        MoveEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        /* prev
        if (!_enemy.IsTargetInInnerRange)
        {
            if (_enemy.IsReachedNexus)
                _stateMachine.ChangeState(EnemyStateType.Reached); 
        }

        else
            _stateMachine.ChangeState(EnemyStateType.Chase); 
            */
        _enemy.MoveToNexus();

        if (_enemy.NavAgent.isOnOffMeshLink)
            _stateMachine.ChangeState(EnemyStateType.Jump);

        Debug.Log(_enemy.NavAgent.isOnOffMeshLink);

        if (_enemy.IsReachedNexus)
            _stateMachine.ChangeState(EnemyStateType.Reached); //���� ��Ÿ� ���� ���Դ� -> Reached (�ؼ��� ����)

        if (_enemy.IsTargetInInnerRange)
            _stateMachine.ChangeState(EnemyStateType.Chase); //���� ��Ÿ� ���� ���Դ� -> chase (�÷��̾� ������)
    }

    public override void ExitState()
    {
        base.ExitState();

        MoveExit();
    }
}
