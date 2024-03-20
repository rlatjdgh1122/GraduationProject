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
        if (_penguin.IsFreelyMove) { return; }
        IdleEnter();
    }
    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        if (_penguin.NavAgent.velocity.magnitude > 0.05f)   
        {
            _stateMachine.ChangeState(MopPenguinStateEnum.Move);
        }

        if (_penguin.IsInnerTargetRange)
            _stateMachine.ChangeState(MopPenguinStateEnum.Chase);

        if (_penguin.IsFreelyMove)
        {
            _stateMachine.ChangeState(MopPenguinStateEnum.FreelyMove);
        }
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
