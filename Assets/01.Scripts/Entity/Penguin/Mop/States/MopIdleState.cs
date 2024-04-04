using UnityEngine;

public class MopIdleState : MopBaseState
{
    public MopIdleState(Penguin penguin, EntityStateMachine<MopPenguinStateEnum, Penguin> stateMachine, string animBoolName)
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

        if (_penguin.NavAgent.velocity.magnitude > 0.05f)
        {
            _stateMachine.ChangeState(MopPenguinStateEnum.Move);
        }

        else if (_penguin.IsInnerTargetRange)
            _stateMachine.ChangeState(MopPenguinStateEnum.Chase);
    }

    public override void Exit()
    {
        IdleExit();

        base.Exit();
    }
}
