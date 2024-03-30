using UnityEngine;

public class BasicIdleState : BasicBaseState
{
    public BasicIdleState(Penguin penguin, EntityStateMachine<BasicPenguinStateEnum, Penguin> stateMachine, string animationBoolName)
        : base(penguin, stateMachine, animationBoolName)
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

        //적사거리가 들어오고 /*-군사들이 위치로 이동했다면-*/ 주석
        if (_penguin.NavAgent.desiredVelocity.magnitude > 0.05f)
            _stateMachine.ChangeState(BasicPenguinStateEnum.Move);
        if (_penguin.IsInnerTargetRange)
            _stateMachine.ChangeState(BasicPenguinStateEnum.Chase);
    }



    public override void Exit()
    {
        SignalHub.OnIceArrivedEvent -= FindTarget;
        IdleExit();

        base.Exit();
    }
}
