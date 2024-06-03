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

        if (_penguin.NavAgent.velocity.magnitude > 0.05f)
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Move);

        if (_penguin.MoveFocusMode == ArmySystem.MovefocusMode.Battle && _penguin.MyArmy.IsArmyReady)
        {
            if (_penguin.IsTargetInInnerRange)
                _stateMachine.ChangeState(ShieldPenguinStateEnum.Chase);
        }
    }

    public override void Exit()
    {
        IdleExit();

        base.Exit();
    }
}
