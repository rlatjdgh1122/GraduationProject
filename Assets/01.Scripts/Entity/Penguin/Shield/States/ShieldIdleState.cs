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

        if(_penguin.IsFreelyMove) { return; }

        IdleEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.NavAgent.velocity.magnitude > 0.05f)
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Move);

        if (_penguin.IsInnerTargetRange)
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Chase);

        if (_penguin.IsFreelyMove)
        {
            _stateMachine.ChangeState(ShieldPenguinStateEnum.FreelyMove);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
