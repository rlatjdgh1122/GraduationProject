
using UnityEngine;

public class EnemyAnimalChaseState : EnemyAnimalBaseState
{
    public EnemyAnimalChaseState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName)
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
            _enemy.MoveToCurrentTarget();

        if (_enemy.CanAttack)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Attack); //���� ��Ÿ� ���� ���Դ� -> Attack

        if (_enemy.IsProvoked)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Provoked); //���ߴ��� �� ����State��

        if (!_enemy.IsTargetPlayerInside)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Move); //�÷��̾� ����� �ƿ� ���� ��Ÿ��� ����� -> �ؼ����� Move
    }

    public override void Exit()
    {
        base.Exit();
    }
}
