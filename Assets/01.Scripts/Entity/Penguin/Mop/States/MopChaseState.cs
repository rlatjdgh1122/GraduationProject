using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class MopChaseState : MopBaseState
{
    public MopChaseState(Penguin penguin, EntityStateMachine<MopPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        ChaseEnter();
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();



    }
    public override void UpdateState()
    {
        base.UpdateState();

        // 그냥 클릭 : 따라가던 도중 마우스 클릭되면 이동
        if (IsArmyCalledIn_CommandMode())
        {
            _stateMachine.ChangeState(MopPenguinStateEnum.MustMove);
        }

        else
        {
            if (_penguin.CurrentTarget != null)
                _penguin.MoveToCurrentTarget();

            if (_penguin.IsInnerMeleeRange)
                _stateMachine.ChangeState(MopPenguinStateEnum.Attack);

            IsTargetNull(MopPenguinStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
