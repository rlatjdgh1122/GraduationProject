using ArmySystem;
using UnityEngine;

public class PenguinLanceSkillState : State
{
    private General General => _penguin as General;

    public PenguinLanceSkillState(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        prevMode = _penguin.MyArmy.MovefocusMode;
        _penguin.MyArmy.MovefocusMode = MovefocusMode.Stop;
        _penguin.StopImmediately();

        _penguin.LookTargetImmediately();

        _triggerCalled = false;
        General.Skill.PlaySkill();
    }

    public override void UpdateState()
    {
        //base.UpdateState();
        /*_penguin.MyArmy.MovefocusMode = MovefocusMode.Stop;
        _penguin.StopImmediately();*/

        /* if (_triggerCalled)
         {
             if (_penguin.IsTargetInAttackRange)
             {
                 _stateMachine.ChangeState(PenguinStateType.Attack);
             }
             else
             {
                 _stateMachine.ChangeState(PenguinStateType.Idle);
             }
         }*/
        if (_triggerCalled)
        {
            if (!_penguin.IsTargetInAttackRange)
            {
                _stateMachine.ChangeState(PenguinStateType.Chase);
            }
            else if (_penguin.IsTargetInAttackRange)
            {
                _stateMachine.ChangeState(PenguinStateType.Attack);
            }
            else IsTargetNull(PenguinStateType.Idle);
        }

    }

    public override void ExitState()
    {
        _penguin.MyArmy.MovefocusMode = prevMode;

        base.ExitState();
    }
}