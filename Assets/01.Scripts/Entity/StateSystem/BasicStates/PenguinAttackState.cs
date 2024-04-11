using UnityEngine;

public class PenguinAttackState : State
{
    private int curAttackCount = 0;

    public override void EnterState()
    {
        base.EnterState();

        AttackEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _penguin.LookTarget();

        if (IsArmyCalledIn_BattleMode())
        {
            if (_triggerCalled)
            {
                if (_penguin.CurrentTarget.IsDead)
                    _stateMachine.ChangeState(PenguinStateType.Chase);

                IsTargetNull(PenguinStateType.MustMove);
            }
        }

        else if (IsArmyCalledIn_CommandMode())
        {
            if (_penguin.WaitForCommandToArmyCalled)
            {
                _stateMachine.ChangeState(PenguinStateType.MustMove);
            }
        }
        else
        {
            if (_triggerCalled) //АјАн
            {
                _stateMachine.ChangeState(PenguinStateType.Chase);

                IsTargetNull(PenguinStateType.Idle);
            }
        }
    }

    public override void ExitState()
    {
        AttackExit();

        base.ExitState();
    }

    public override void AnimationTrigger()
    {
        base.AnimationTrigger();

        if (_penguin.CheckAttackEventPassive(++curAttackCount))
        {
            _penguin?.OnPassiveAttackEvent();
        }
    }
}
