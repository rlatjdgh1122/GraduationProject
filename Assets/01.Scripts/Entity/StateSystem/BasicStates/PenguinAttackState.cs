using ArmySystem;
using UnityEngine;

public class PenguinAttackState : State
{

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
            if (!_penguin.IsTargetInAttackRange)
            {
                _stateMachine.ChangeState(PenguinStateType.Chase);
            }
            else
            {
                IsTargetNull(PenguinStateType.Idle);
            }
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

        int curCount = _entityActionData.AddAttackCount();

        if (_penguin.CheckAttackPassive(curCount))
        {
            _penguin?.OnPassiveAttackEvent();
        }
    }
}
