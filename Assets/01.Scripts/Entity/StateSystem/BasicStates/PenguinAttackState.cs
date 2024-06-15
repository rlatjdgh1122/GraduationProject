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
            IsTargetNull(PenguinStateType.Idle);
        }

        CheckCommandModeForMovement();
        CheckCommandModeForChase();
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
            Debug.Log((this as State).GetHashCode());
        }
    }
}
