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

        if (_triggerCalled) //공격이 한 차례 끝났을 때,
        {
            if (_enemy.IsTargetPlayerInside)
                _stateMachine.ChangeState(EnemyPenguinStateEnum.Chase); //사거리 안에 타겟 플레이어가 있다 -> 따라가
            else
                _stateMachine.ChangeState(EnemyPenguinStateEnum.Move); //없다 -> 넥서스로 Move

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