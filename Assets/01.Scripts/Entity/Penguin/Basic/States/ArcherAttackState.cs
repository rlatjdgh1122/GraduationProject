using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAttackState : ArcherBaseState
{
    public ArcherAttackState(Penguin penguin, EntityStateMachine<ArcherPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();

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
                _stateMachine.ChangeState(ArcherPenguinStateEnum.Chase);
                //���׿��ٸ� �̵�
                IsTargetNull(ArcherPenguinStateEnum.MustMove);
            }
        }

        if (IsArmyCalledIn_CommandMode())
        {
            if (_penguin.WaitForCommandToArmyCalled)
            {
                _stateMachine.ChangeState(ArcherPenguinStateEnum.MustMove);
            }
        }

        else
        {
            if (_triggerCalled) //����
            {
                _stateMachine.ChangeState(ArcherPenguinStateEnum.Chase);

                IsTargetNull(ArcherPenguinStateEnum.Idle);
            }
        }
    }

    public override void Exit()
    {
        _penguin.AnimatorCompo.speed = 1;

        base.Exit();
    }
}
