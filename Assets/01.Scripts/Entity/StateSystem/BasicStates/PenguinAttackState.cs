using ArmySystem;
using UnityEngine;

public class PenguinAttackState : State
{
    private int curAttackCount = 0;

    public PenguinAttackState(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        AttackEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _penguin.LookTarget();

        if (_triggerCalled) //АјАн
        {
            _stateMachine.ChangeState(PenguinStateType.Chase);

            IsTargetNull(PenguinStateType.Idle);
        }

    }

    public override void ExitState()
    {
        base.ExitState();

        AttackExit();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        if (_penguin.MoveFocusMode == MovefocusMode.Command)
        {
            _stateMachine.ChangeState(PenguinStateType.Idle);
        }

        if (_penguin.CheckAttackPassive(++curAttackCount))
        {
            _penguin?.OnPassiveAttackEvent();

            curAttackCount = 0;
        }
    }
}
