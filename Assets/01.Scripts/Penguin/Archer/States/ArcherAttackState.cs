using UnityEngine;

public class ArcherAttackState : ArcherBaseState
{
    public ArcherAttackState(Penguin penguin, PenguinStateMachine<ArcherPenguinStateEnum> stateMachine, string animationBoolName)
        : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _penguin.StopImmediately();
        _triggerCalled = false;
        _penguin.AnimatorCompo.speed = _penguin.attackSpeed;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _penguin.StopImmediately();
        _penguin.LookTarget();

        if (_triggerCalled && _penguin.Target != null)
        {
            if (_penguin.IsInTargetRange)
            {
                _stateMachine.ChangeState(ArcherPenguinStateEnum.Chase);
            }
            else
            {
                _stateMachine.ChangeState(ArcherPenguinStateEnum.Idle);
            }

            if (_penguin.Target == null) //Ÿ���� ���ٸ� ������ ����
            {
                _stateMachine.ChangeState(ArcherPenguinStateEnum.Idle);
            }
        }
    }

    public override void Exit()
    {
        _penguin.AnimatorCompo.speed = 1;
        base.Exit();
    }
}