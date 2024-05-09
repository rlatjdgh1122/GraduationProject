using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBaseState : EnemyState
{
    private AnimalAttackableEntity _animalAttack;
    private readonly int _comboCounterHash = Animator.StringToHash("ComboCounter");

    public EnemyBaseState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
        if(enemy.TryGetComponent<AnimalAttackableEntity>(out var component))
        {
            _animalAttack = component;
        }
    }

    #region Enter
    protected void IdleEnter()
    {
        _triggerCalled = true;
    }

    protected void AttackEnter()
    {
        if (_enemy.CurrentTarget != null)
            _enemy.CurrentTarget.HealthCompo.OnDied += DeadTarget;

        _triggerCalled = false;
        _enemy.StopImmediately();
        _enemy.AnimatorCompo.speed = _enemy.attackSpeed;
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

    protected void ChaseEnter()
    {
        _triggerCalled = true;

        _enemy.FindNearestTarget();

        if (_enemy.CurrentTarget != null)
        {
            _navAgent.ResetPath();
            _enemy.MoveToCurrentTarget();
        }
    }

    protected void MustChaseEnter()
    {
        _triggerCalled = true;

        _enemy.FindNearestTarget();

        if (_enemy.CurrentTarget != null)
            _enemy.MoveToCurrentTarget();
    }

    protected void MoveEnter()
    {
        _enemy.MoveToNexus();
    }

    protected void ChestHitEnter()
    {
        _enemy.StopImmediately();
        _triggerCalled = false;
    }
    #endregion

    #region Exit
    protected void IdleExit()
    {
    }

    protected void AttackExit()
    {
        _enemy.AnimatorCompo.speed = 1;
    }

    protected void AttackComboExit()
    {
        ++_animalAttack.ComboCounter;
        _animalAttack.LastAttackTime = Time.time;

        _enemy.AnimatorCompo.speed = 1;
    }

    protected void ChaseExit()
    {

    }

    protected void MustChaseExit()
    {

    }

    protected void MoveExit()
    {

    }

    protected void ChestHitExit()
    {
        _triggerCalled = false;
    }
    #endregion

    #region Method
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
}
