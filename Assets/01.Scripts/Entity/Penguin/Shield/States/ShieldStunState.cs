using System.Linq;
using Unity.VisualScripting;

public class ShieldStunState : ShieldBaseState
{

    public ShieldStunState(Penguin penguin, EntityStateMachine<ShieldPenguinStateEnum, Penguin> stateMachine, string animationBoolName)
        : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter() //�Ѹ��� �����ٰ� ������ 
    {
        base.Enter();
        _triggerCalled = false;
        _penguin.WaitForCommandToArmyCalled = false;
        _penguin.FindFirstNearestEnemy();
        _penguin.StopImmediately();
        _penguin.AnimatorCompo.speed = _penguin.attackSpeed;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _penguin.LookTarget();

        if (IsArmyCalledIn_BattleMode())
        {
            if (_triggerCalled)
            {
                _stateMachine.ChangeState(ShieldPenguinStateEnum.Chase);
                //���׿��ٸ� �̵�
                IsTargetNull(ShieldPenguinStateEnum.MustMove);
            }
        }
        if (IsArmyCalledIn_CommandMode())
        {
            if (_penguin.WaitForCommandToArmyCalled)
            {
                _stateMachine.ChangeState(ShieldPenguinStateEnum.MustMove);
            }
        }

        if (_triggerCalled)
        {
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Chase);

            if (_penguin.CurrentTarget == null)
                _stateMachine.ChangeState(ShieldPenguinStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        _penguin.AnimatorCompo.speed = 1;
        base.Exit();
    }
}
