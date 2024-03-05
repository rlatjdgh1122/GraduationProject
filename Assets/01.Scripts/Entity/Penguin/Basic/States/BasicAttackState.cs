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
        _triggerCalled = false;
        _penguin.WaitTrueAnimEndTrigger = false;

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

        if (_penguin.ArmyTriggerCalled)
        {
            float animTime =
                _penguin.AnimatorCompo.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (animTime >= 1f)
            {
                Debug.Log("애니메이션 끝");
                _penguin.WaitTrueAnimEndTrigger = true;
                _penguin.MoveToTarget(_penguin.GetSeatPosition());
                //_stateMachine.ChangeState(BasicPenguinStateEnum.Move);
            }
        }
    }

    public override void Exit()
    {
        _penguin.AnimatorCompo.speed = 1;
        _penguin.Owner.IsMoving = true;
        base.Exit();
    }
}
