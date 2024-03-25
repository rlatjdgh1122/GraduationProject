using UnityEngine;

public class EnemyAnimalAttackState : EnemyAnimalBaseState
{
    private AnimalAttackableEntity _animalAttack;
    private readonly int _comboCounterHash = Animator.StringToHash("ComboCounter");

    public EnemyAnimalAttackState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
        _animalAttack = enemyBase.GetComponent<AnimalAttackableEntity>();
    }

    public override void Enter()
    {
        base.Enter();
        if (_animalAttack.ComboCounter > _animalAttack.animalAttack.Count - 1
            || Time.time >= _animalAttack.LastAttackTime + _animalAttack.ComboWindow)
        {
            _animalAttack.ComboCounter = 0; //콤보 초기화 조건에 따라 콤보 초기화
        }
        _enemy.AnimatorCompo.SetInteger(_comboCounterHash, _animalAttack.ComboCounter);

        _enemy.StopImmediately();

        _enemy.AnimatorCompo.speed = _enemy.attackSpeed;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if(_enemy.CurrentTarget.IsDead)
        {
            _enemy.CurrentTarget = null;
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
