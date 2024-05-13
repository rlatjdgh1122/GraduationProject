using UnityEngine;
using UnityEngine.AI;

public class EnemyJumpState : EnemyBaseState
{
    public EnemyJumpState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _triggerCalled = false;

        Debug.Log("а║га╥н ©х");
        _enemy.MoveToNexus();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_triggerCalled)
            _stateMachine.ChangeState(EnemyStateType.Move);
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
