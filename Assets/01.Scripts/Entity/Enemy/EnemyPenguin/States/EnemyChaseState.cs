
using UnityEngine;

public class EnemyChaseState : EnemyBaseState
{
    public EnemyChaseState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }
    public override void EnterState()
    {
        base.EnterState();

        ChaseEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

      

        if (_enemy.IsTargetInAttackRange)
            _stateMachine.ChangeState(EnemyStateType.Attack); //공격 사거리 내에 들어왔다 -> Attack
       /* else
            _stateMachine.ChangeState(EnemyPenguinStateEnum.Chase); //공격 사거리 밖이면 계속 따라가*/

       /* if (!_enemy.IsTargetInInnerRange)
            _stateMachine.ChangeState(EnemyStateType.Move); //플레이어 펭귄이 아예 감지 사거리를 벗어났다 -> 넥서스로 Move*/
    }

    public override void ExitState()
    {
        base.ExitState();

        ChaseExit();
    }
}
