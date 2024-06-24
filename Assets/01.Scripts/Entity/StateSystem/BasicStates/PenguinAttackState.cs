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
            if (_penguin.IsTargetInAttackRange)
            {
                _stateMachine.ChangeState(PenguinStateType.Chase);
            }
            else IsTargetNull(PenguinStateType.Idle);

        }

        CheckCommandModeForMovement();
        CheckBattleModeForChase();
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
