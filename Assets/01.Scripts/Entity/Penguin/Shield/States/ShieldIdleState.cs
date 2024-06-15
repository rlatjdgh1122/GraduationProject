using UnityEngine;

public class ShieldIdleState : ShieldBaseState
{
    public ShieldIdleState(Penguin penguin, EntityStateMachine<ShieldPenguinStateEnum, Penguin> stateMachine, string animBoolName)
        : base(penguin, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();   

        IdleEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        CheckCommandModeForChase();
        CheckCommandModeForMovement();
    }

    public override void Exit()
    {
        IdleExit();

        base.Exit();
    }
}
