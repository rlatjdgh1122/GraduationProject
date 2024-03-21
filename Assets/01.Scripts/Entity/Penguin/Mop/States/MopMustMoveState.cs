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
    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        if (_penguin.WaitForCommandToArmyCalled)
        {
            if (_penguin.NavAgent.velocity.magnitude < 0.05f)
            {
                _stateMachine.ChangeState(MopPenguinStateEnum.Idle);
            }
        }

        if (_penguin.IsInnerTargetRange
            && _penguin.MoveFocusMode == MovefocusMode.Battle)
        {
            Debug.Log(_penguin.IsInnerTargetRange + " : " + _penguin.MoveFocusMode);
            _stateMachine.ChangeState(MopPenguinStateEnum.Chase);
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
