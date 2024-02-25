using System.Linq;
using Unity.VisualScripting;

public class BasicAttackState : BasicBaseState
{
    public BasicAttackState(Penguin penguin, PenguinStateMachine<BasicPenguinStateEnum> stateMachine, string animationBoolName)
        : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter() //한명이 때리다가 죽으면 
    {
        base.Enter();
        _triggerCalled = false;
        _penguin.FindFirstNearestEnemy();
        _penguin.Owner.IsMoving = false;
        _penguin.StopImmediately();
        _penguin.AnimatorCompo.speed = _penguin.attackSpeed;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _penguin.LookTarget();

        if (_triggerCalled)
        {
            _stateMachine.ChangeState(BasicPenguinStateEnum.Chase);

            if (_penguin.CurrentTarget == null)
                _stateMachine.ChangeState(BasicPenguinStateEnum.Idle);
        }
    }

    public override void Exit()
    {
        _penguin.AnimatorCompo.speed = 1;
        _penguin.Owner.IsMoving = true;
        base.Exit();
    }
}
