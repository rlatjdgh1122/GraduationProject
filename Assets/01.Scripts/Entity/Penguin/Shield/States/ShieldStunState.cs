using ArmySystem;
using System.Linq;
using Unity.VisualScripting;

public class ShieldStunState : ShieldBaseState
{

    public ShieldStunState(Penguin penguin, EntityStateMachine<ShieldPenguinStateEnum, Penguin> stateMachine, string animationBoolName)
        : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter() //한명이 때리다가 죽으면 
    {
        base.Enter();

        AttackEnter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _penguin.LookTarget();

        if(_penguin.MoveFocusMode == MovefocusMode.Command)
        {
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Idle);
        }
        else
        {
            if (_triggerCalled)
            {
                _stateMachine.ChangeState(ShieldPenguinStateEnum.Chase);

                if (_penguin.CurrentTarget == null)
                    _stateMachine.ChangeState(ShieldPenguinStateEnum.Idle);
            }
        }

    }

    public override void Exit()
    {
        _penguin.AnimatorCompo.speed = 1;
        base.Exit();
    }
}
