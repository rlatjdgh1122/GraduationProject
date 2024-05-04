/*using UnityEngine;

public class EnemyAnimalAttackState : EnemyAnimalBaseState
{
    public EnemyAnimalAttackState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {

    }

    public override void EnterState()
    {
        base.EnterState();

        AttackComboEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _enemy.LookTarget();

        if (_enemy.CurrentTarget.IsDead)
        {
            _enemy.CurrentTarget = null;

            _stateMachine.ChangeState(EnemyPenguinStateEnum.Move);
        }

        if (_triggerCalled) //������ �� ���� ������ ��,
        {
            if (_enemy.IsTargetPlayerInside)
                _stateMachine.ChangeState(EnemyPenguinStateEnum.Chase); //��Ÿ� �ȿ� Ÿ�� �÷��̾ �ִ� -> ����
            else
                _stateMachine.ChangeState(EnemyPenguinStateEnum.Move); //���� -> �ؼ����� Move

            _triggerCalled = false;
        }
    }

    public override void Exit()
    {
        ++_animalAttack.ComboCounter;
        _animalAttack.LastAttackTime = Time.time;

        _enemy.AnimatorCompo.speed = 1;
        base.Exit();
    }
}
*/