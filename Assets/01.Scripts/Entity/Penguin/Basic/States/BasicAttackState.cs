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
        _penguin.StopImmediately();
        _penguin.AnimatorCompo.speed = _penguin.attackSpeed;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _penguin.LookTarget();

        if (_penguin.ArmyTriggerCalled)
        {
            float animTime =
                _penguin.AnimatorCompo.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (animTime >= 1f) //애니메이션 끝나느 부분이죠~
            {
                _penguin.WaitTrueAnimEndTrigger = true;
                _stateMachine.ChangeState(BasicPenguinStateEnum.Move);
            }
        }
        else
        {
            if (_triggerCalled)
            {
                _stateMachine.ChangeState(BasicPenguinStateEnum.Chase);

                if (_penguin.CurrentTarget == null)
                    _stateMachine.ChangeState(BasicPenguinStateEnum.Idle);
            }
        }
    }

    public override void Exit()
    {
        _penguin.AnimatorCompo.speed = 1;
        base.Exit();
    }
}
