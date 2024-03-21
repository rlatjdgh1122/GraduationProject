
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

        DeadEnter();
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
