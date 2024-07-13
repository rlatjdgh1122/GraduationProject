using ArmySystem;

public class PenguinChaseState : State
{
    public PenguinChaseState(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        ChaseEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();


        //�̵����°� �Ȱ� �ƴ϶�� ������ Ÿ������ ����
        if (!CheckCommandModeForMovement())
        {
            if (_penguin.CurrentTarget != null)
                _penguin.MoveToCurrentTarget();
        }


        if (!CheckBattleModeForChase())
        {
            if (_penguin.IsTargetInAttackRange)
            {
                _stateMachine.ChangeState(PenguinStateType.Attack);
            }
           /* else if (_stateMachine.IsPrevState(PenguinStateType.Attack) == false)
            {
                if (_penguin.MovefocusMode == MovefocusMode.Stop)
                {
                    _stateMachine.ChangeState(PenguinStateType.Idle);
                }
            }*/
            else IsTargetNull(PenguinStateType.Idle);
        }

        IsTargetNull(PenguinStateType.Idle);

    }

    public override void ExitState()
    {
        base.ExitState();

        ChaseExit();
    }


}