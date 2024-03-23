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
        Debug.Log(_navAgent.velocity.magnitude);
    }
    public override void FixedUpdateState()
    {
        base.FixedUpdateState();
    }
    public override void UpdateState()
    {
        base.UpdateState();

        Debug.Log(_navAgent.velocity.magnitude);
        if (_penguin.WaitForCommandToArmyCalled)
        {
            if (_penguin.NavAgent.desiredVelocity.magnitude < 0.05f)
            {
                Debug.Log("머스트 아이들 : " + _navAgent.velocity.magnitude);
                _stateMachine.ChangeState(MopPenguinStateEnum.Idle);
            }
        }

        else if (_penguin.IsInnerTargetRange
            && _penguin.MoveFocusMode == MovefocusMode.Battle)
        {
            // Debug.Log(_penguin.IsInnerTargetRange + " : " + _penguin.MoveFocusMode);
            _stateMachine.ChangeState(MopPenguinStateEnum.Chase);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

}
