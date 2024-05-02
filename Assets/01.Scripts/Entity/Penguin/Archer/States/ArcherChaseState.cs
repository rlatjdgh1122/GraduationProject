using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherChaseState : ArcherBaseState
{
    public ArcherChaseState(Penguin penguin, EntityStateMachine<ArcherPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }


    public override void Enter()
    {
        base.Enter();

        ChaseEnter();
    }
    public override void UpdateState()
    {
        base.UpdateState();

        // �׳� Ŭ�� : ���󰡴� ���� ���콺 Ŭ���Ǹ� �̵�
        if (IsArmyCalledIn_CommandMode())
        {
            _stateMachine.ChangeState(ArcherPenguinStateEnum.MustMove);
        }
        else
        {
            if (_penguin.CurrentTarget != null)
                _penguin.MoveToCurrentTarget();

            if (_penguin.IsTargetInAttackRange)
                _stateMachine.ChangeState(ArcherPenguinStateEnum.Attack);

            else IsTargetNull(ArcherPenguinStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
