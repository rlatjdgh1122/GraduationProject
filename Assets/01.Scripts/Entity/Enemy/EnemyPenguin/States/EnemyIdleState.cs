using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public EnemyIdleState(Enemy enemy, EnemyStateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        IdleEnter();

        SignalHub.OnIceArrivedEvent += ChangeState;
    }

    public override void UpdateState()
    {
        //if (_enemy.NavAgent.isOnOffMeshLink)
        //    _stateMachine.ChangeState(EnemyStateType.Jump);

        base.UpdateState();
    }

    private void ChangeState()
    {
        _enemy.NavAgent.enabled = true;
        _enemy.FindNearestTarget();

        _stateMachine.ChangeState(EnemyStateType.Move);
    }

    public override void ExitState()
    {
        base.ExitState();

        IdleExit();

        SignalHub.OnIceArrivedEvent -= ChangeState;
    }
}
