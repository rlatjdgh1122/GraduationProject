using UnityEngine;

public class PenguinStunState : State
{
    public PenguinStunState(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _triggerCalled = false;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_triggerCalled)
        {
            if (_penguin.CurrentTarget != null)
                _penguin.RunSecondPassive();
            _stateMachine.ChangeState(PenguinStateType.Throw); //Throw·Î
        }
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
