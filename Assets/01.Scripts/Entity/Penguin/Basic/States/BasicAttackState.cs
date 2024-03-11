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
        _penguin.WaitForCommandToArmyCalled = false;
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
            if (_penguin.MoveFocusMode == MovefocusMode.Battle)
            {
                if (_triggerCalled)
                {
                    _stateMachine.ChangeState(BasicPenguinStateEnum.Chase);
                    //다죽였다면 이동
                    if (_penguin.CurrentTarget == null)
                    {
                        _stateMachine.ChangeState(BasicPenguinStateEnum.MustMove);
                    }
                }
            }
            else if (_penguin.MoveFocusMode == MovefocusMode.Command)
            {
                if (_penguin.WaitForCommandToArmyCalled)
                {
                    Debug.Log("이동해라");
                    _stateMachine.ChangeState(BasicPenguinStateEnum.MustMove);
                    Debug.Log("이동해라2");
                }
            }

        }
        else
        {
            if (_triggerCalled) //공격
            {
                _stateMachine.ChangeState(BasicPenguinStateEnum.Chase);

                if (_penguin.CurrentTarget == null)
                {
                    _stateMachine.ChangeState(BasicPenguinStateEnum.Idle);
                }

            }
        }
    }

    public override void Exit()
    {
        _penguin.NavAgent.velocity = Vector3.one * .5f;
        _penguin.AnimatorCompo.speed = 1;
        base.Exit();
    }
}
