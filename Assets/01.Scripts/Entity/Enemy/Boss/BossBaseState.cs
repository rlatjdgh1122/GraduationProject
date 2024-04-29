using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBaseState<T> : EnemyState<T> where T : Enum
{
    protected AnimalAttackableEntity _animalAttack;
    protected int AttackCount = 0;

    private readonly int _comboCounterHash = Animator.StringToHash("ComboCounter");
    private readonly string _mustMoveHash = "ComboCounter";
    public BossBaseState(Enemy enemyBase, EnemyStateMachine<T> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
        _animalAttack = enemyBase.GetComponent<AnimalAttackableEntity>();
    }

    #region Enter

    protected void IdleEnter()
    {
        _triggerCalled = true;
    }
    protected void ChaseEnter()
    {
        _triggerCalled = true;
    }

    protected void MoveEnter()
    {
        _triggerCalled = true;
        _enemy.StopImmediately(); //움직이면서 공격하는거 방지

        _enemy.FindNearestTarget();
    }
    protected void MustMoveEnter()
    {
        _triggerCalled = true;
        _enemy.FindNearestTarget();
    }
    protected void ProvokedEnter()
    {
        //_enemy.CurrentTarget = _enemy.FindNearestPenguin<ITar>();
    }
    protected void AttackComboEnter()
    {
        if (_animalAttack.ComboCounter > _animalAttack.animalAttackList.Count - 1
            || Time.time >= _animalAttack.LastAttackTime + _animalAttack.ComboWindow)
        {
            _animalAttack.ComboCounter = 0; //콤보 초기화 조건에 따라 콤보 초기화
        }
        _enemy.AnimatorCompo.SetInteger(_comboCounterHash, _animalAttack.ComboCounter);

        AttackEnter();
    }

    protected void AttackEnter()
    {
        if (_enemy.CurrentTarget != null)
            _enemy.CurrentTarget.HealthCompo.OnDied += DeadTarget;

        _enemy.StopImmediately();
        _enemy.AnimatorCompo.speed = _enemy.attackSpeed;
    }

    protected void ReachedEnter()
    {
        _triggerCalled = false;
        _enemy.FindNearestTarget();
        _enemy.HealthCompo.OnHit += ChangeStateWhenHitted;
        _enemy.StopImmediately();
        _enemy.AnimatorCompo.speed = _enemy.attackSpeed;
    }
    #endregion

    #region Exit

    protected void AttackComboExit()
    {
        ++_animalAttack.ComboCounter;
        _animalAttack.LastAttackTime = Time.time;

        AttackExit();
    }
    protected void AttackExit()
    {
        _enemy.AnimatorCompo.speed = 1;
        if (_enemy.CurrentTarget != null)
            _enemy.CurrentTarget.HealthCompo.OnDied -= DeadTarget;
    }
    protected void ReachedExit()
    {
        _enemy.HealthCompo.OnHit -= ChangeStateWhenHitted;
        _enemy.AnimatorCompo.speed = 1;
    }

    #endregion

    #region Handler
    public void ChangeStateWhenHitted()
    {
        //MustChase가 있다면
        try
        {
            _stateMachine.ChangeState((T)Enum.Parse(typeof(T), _mustMoveHash));
        }
        catch (Exception e)
        {
            Debug.Log($"{typeof(T).Name} 타입에 {_mustMoveHash}가 존재하지 않습니다.");
        }
    }

    private void DeadTarget()
    {
        var prevTarget = _enemy.CurrentTarget;

        _enemy.FindNearestTarget();

        if (prevTarget != null)
        {
            prevTarget.HealthCompo.OnDied -= DeadTarget;
        }
        if (_enemy.CurrentTarget != null)
        {
            _enemy.CurrentTarget.HealthCompo.OnDied += DeadTarget;
        }
    }

    #endregion

    #region Etc

    protected void InitAttackCount()
    {
        AttackCount = 0;
    }
    #endregion
}
