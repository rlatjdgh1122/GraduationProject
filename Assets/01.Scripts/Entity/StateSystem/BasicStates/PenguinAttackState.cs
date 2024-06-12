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

        if (_penguin.ArmyTriggerCalled)
        {
            _penguin.StateMachine.ChangeState(PenguinStateType.MustMove);
        }

        CheckCommandModeForMovement();
    }

    public override void ExitState()
    {
        base.ExitState();

        AttackExit();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        if (_penguin.CheckAttackPassive(++curAttackCount))
        {
            _penguin?.OnPassiveAttackEvent();

            curAttackCount = 0;
        }
    }
}
