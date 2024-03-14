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
        _triggerCalled = true;

        _penguin.ArmyTriggerCalled = false;
        _penguin.SuccessfulToArmyCalled = true;
        _penguin.WaitForCommandToArmyCalled = true;

        if (_penguin.MoveFocusMode == MovefocusMode.Battle)
            _penguin.NavAgent.ResetPath();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.NavAgent.velocity.magnitude > 0.05f)
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Move);

        if (_penguin.IsInnerTargetRange)
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Chase);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
