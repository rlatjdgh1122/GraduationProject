using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class BasicAttackState : BasicBaseState
{
    public BasicAttackState(Penguin penguin, EntityStateMachine<BasicPenguinStateEnum, Penguin> stateMachine, string animationBoolName)
        : base(penguin, stateMachine, animationBoolName)
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
                _stateMachine.ChangeState(BasicPenguinStateEnum.Chase);
                //���׿��ٸ� �̵�
                IsTargetNull(BasicPenguinStateEnum.MustMove);
            }
        }

        if (IsArmyCalledIn_CommandMode())
        {
            if (_penguin.WaitForCommandToArmyCalled)
            {
                _stateMachine.ChangeState(BasicPenguinStateEnum.MustMove);
            }
        }

        else
        {
            if (_triggerCalled) //����
            {
                _stateMachine.ChangeState(BasicPenguinStateEnum.Chase);

                IsTargetNull(BasicPenguinStateEnum.Idle);
            }
        }
    }

    public override void Exit()
    {
        _penguin.AnimatorCompo.speed = 1;
        base.Exit();
    }
}
