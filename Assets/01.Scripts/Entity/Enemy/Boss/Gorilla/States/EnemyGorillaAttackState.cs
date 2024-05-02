/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGorillaAttackState : EnemyGorillaBaseState
{
    public EnemyGorillaAttackState(Enemy enemyBase, EnemyStateMachine<EnemyGorillaStateEnum> stateMachine, string animBoolName) : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AttackEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _enemy.LookTarget();

        if (_triggerCalled) //공격이 한 차례 끝났을 때,
        {
            if (!_enemy.IsTargetPlayerInside)
                _stateMachine.ChangeState(EnemyGorillaStateEnum.Chase); //사거리 안에 타겟 플레이어가 있다 -> 따라가
            else if (_enemy.CurrentTarget == null)
                _stateMachine.ChangeState(EnemyGorillaStateEnum.Move); //없다 -> 넥서스로 Move

            _triggerCalled = false;
        }
    }

    public override void Exit()
    {
        base.Exit();

        AttackExit();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();

        if (_enemy.CheckAttackEventPassive(++AttackCount))
        {
            _enemy?.OnPassiveAttackEvent();

            AttackCount = 0;
        }
    }
}
*/