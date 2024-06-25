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

        //무조건 현재 적한테 이동
        if (_penguin.CurrentTarget != null)
            _penguin.MoveToCurrentTarget();

        if (_penguin.IsTargetInAttackRange)
        {
            _stateMachine.ChangeState(PenguinStateType.Attack);
        }
        else if (_stateMachine.IsPrevState(PenguinStateType.Attack) == false)
        {
            if (_penguin.MovefocusMode == MovefocusMode.Stop)
            {
                _stateMachine.ChangeState(PenguinStateType.Idle);
            }
        }
        else IsTargetNull(PenguinStateType.Idle);

       /* if (_stateMachine.IsPrevState(PenguinStateType.Attack) == false) //이전 상태가 attack이 아니라면
        {
            if (_penguin.MovefocusMode == MovefocusMode.Stop)
            {
                _stateMachine.ChangeState(PenguinStateType.Idle);
            }

        }//end if*/

        CheckCommandModeForMovement();
        CheckBattleModeForChase();
    }

    public override void ExitState()
    {
        base.ExitState();

        ChaseExit();
    }


}