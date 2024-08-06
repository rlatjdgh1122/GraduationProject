using ArmySystem;
using System.Diagnostics;
using System.Security.Cryptography;

public class PenguinKatanaSkillState : State
{
    private General General => _penguin as General;

    public PenguinKatanaSkillState(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _penguin.StopImmediately();

        _penguin.LookTargetImmediately();

        _triggerCalled = false;
        _penguin.IgnoreToArmyCalled = true;
        General.Skill.PlaySkill();
    }

    public override void UpdateState()
    {
        base.UpdateState();

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
        if (_penguin.MyArmy.MovefocusMode == MovefocusMode.Command) //애니메이션 끝난 이후 움직이게
        {
            _penguin.MoveToClickPositon();
        }
        else if (_penguin.MyArmy.MovefocusMode == MovefocusMode.MustMove)
        {
            _penguin.MustMoveToTargetPostion();
        }

        _penguin.IgnoreToArmyCalled = false;

        base.ExitState();
    }
}