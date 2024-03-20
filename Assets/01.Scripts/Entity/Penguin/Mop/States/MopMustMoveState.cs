using UnityEngine;

public class MopMustMoveState : MopBaseState
{
    public MopMustMoveState(Penguin penguin, EntityStateMachine<MopPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _navAgent.ResetPath();
        _penguin.MoveToPosition(_penguin.GetSeatPosition());
    }
    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        if (_penguin.WaitForCommandToArmyCalled)
        {
            if (_penguin.NavAgent.velocity.magnitude < 0.05f)
            {
                Debug.Log(_penguin.NavAgent.velocity.magnitude);
                Debug.Log("³»°¡ ¹®Á¨°¡..");
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
