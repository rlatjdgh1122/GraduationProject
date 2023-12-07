using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasicBaseState : EnemyState<EnemyPenguinStateEnum>
{
    public EnemyBasicBaseState(Enemy enemyBase, EnemyStateMachine<EnemyPenguinStateEnum> stateMachine, string animBoolName) 
        : base(enemyBase, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _enemy.HealthCompo.OnHit += ChangeStateWhenHitted;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_enemy.IsDead)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Dead); //Á×À¸¸é Dead State·Î

        if (_enemy.Target == null)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Move); //¸Ê¿¡ Å¸°Ù ÇÃ·¹ÀÌ¾î°¡ ¾Æ¿¹ ¾ø´Ù -> ³Ø¼­½º·Î Move
    }

    public void ChangeStateWhenHitted()
    {
        if (_triggerCalled)
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Chase);
    }

    public override void Exit()
    {
        base.Exit();
        _enemy.HealthCompo.OnHit -= ChangeStateWhenHitted;
    }
}
