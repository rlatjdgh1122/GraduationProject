using UnityEngine;

public class MopMustMoveState : MopBaseState
{
    public MopMustMoveState(Penguin penguin, EntityStateMachine<MopPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _penguin.StartImmediately();
        _penguin.MoveToPosition(_penguin.GetSeatPosition());
    }
    public override void UpdateState()
    {
        base.UpdateState();

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

    public override void Exit()
    {
        base.Exit();
    }

}