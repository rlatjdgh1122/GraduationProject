using Unity.VisualScripting;

public class MopAttackState : MopBaseState
{

    private int curAttackCount = 0;
    public MopAttackState(Penguin penguin, EntityStateMachine<MopPenguinStateEnum, Penguin> stateMachine, string animBoolName)
        : base(penguin, stateMachine, animBoolName)
    {
    }

    public override void Enter() //한명이 때리다가 죽으면 
    {
        base.Enter();
        _triggerCalled = false;
        _penguin.WaitForCommandToArmyCalled = false;
        _penguin.FindFirstNearestEnemy();
        _penguin.StopImmediately();
        _penguin.AnimatorCompo.speed = _penguin.attackSpeed;

        if (_penguin.CheckAttackEventPassive(++curAttackCount))
        {
            _penguin?.OnPassiveAttackEvent();
        }
    }

    public override void FixedUpdateState()
    {
        base.FixedUpdateState();

        if (IsArmyCalledIn_BattleMode())
        {
            if (_triggerCalled)
            {
                _stateMachine.ChangeState(MopPenguinStateEnum.Chase);
                //다죽였다면 이동
                IsTargetNull(MopPenguinStateEnum.MustMove);
            }
        }

        if (IsArmyCalledIn_CommandMode())
        {
            if (_penguin.WaitForCommandToArmyCalled)
            {
                _stateMachine.ChangeState(MopPenguinStateEnum.MustMove);
            }
        }
        else
        {
            if (_triggerCalled) //공격
            {
                _stateMachine.ChangeState(MopPenguinStateEnum.Chase);

                IsTargetNull(MopPenguinStateEnum.Idle);
            }
        }
    }
    public override void UpdateState()
    {
        base.UpdateState();

        _penguin.LookTarget();
    }

    public override void Exit()
    {
        _penguin.AnimatorCompo.speed = 1;
        base.Exit();
    }
}
