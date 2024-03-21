using UnityEngine;

public class EnemyAnimalAttackState : EnemyAnimalBaseState
{
    private int _comboCounter; //현재 콤보 수치
    private float _lastAttackTime; //마지막으로 공격했던 시간
    private float _comboWindow = 0.3f; //초기화 쿨타임
    private readonly int _comboCounterHash = Animator.StringToHash("ComboCounter");

    public EnemyAnimalAttackState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName)
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (_comboCounter > 3 || Time.time >= _lastAttackTime + _comboWindow)
        {
            _comboCounter = 0; //콤보 초기화 조건에 따라 콤보 초기화
        }
        _enemy.AnimatorCompo.SetInteger(_comboCounterHash, _comboCounter);

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
        ++_comboCounter;
        _lastAttackTime = Time.time;

        _enemy.AnimatorCompo.speed = 1;
        base.Exit();
    }
}
