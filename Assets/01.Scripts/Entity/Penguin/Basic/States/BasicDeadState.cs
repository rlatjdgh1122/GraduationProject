
using UnityEngine;

public class BasicDeadState : BasicBaseState
{
    public BasicDeadState(Penguin penguin, EntityStateMachine<BasicPenguinStateEnum, Penguin> stateMachine, string animBoolName) 
        : base(penguin, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _triggerCalled = true;
        _penguin.CurrentTarget = null;
        _penguin.StopImmediately();
        _penguin.NavAgent.enabled = false;
        _penguin.enabled = false;
        SignalHub.OnEnemyPenguinDead?.Invoke();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
