using UnityEngine;

public class MopMustMoveState : MopBaseState
{
    public MopMustMoveState(Penguin penguin, EntityStateMachine<MopPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        MustMoveEnter();
    }
    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.WaitForCommandToArmyCalled)
        {
            if (_penguin.NavAgent.remainingDistance < 0.05f)
            {
                _stateMachine.ChangeState(MopPenguinStateEnum.Idle);
            }
        }

        else if (_penguin.IsInnerTargetRange
            && _penguin.MoveFocusMode == MovefocusMode.Battle)
        {
            _stateMachine.ChangeState(MopPenguinStateEnum.Chase);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

}
