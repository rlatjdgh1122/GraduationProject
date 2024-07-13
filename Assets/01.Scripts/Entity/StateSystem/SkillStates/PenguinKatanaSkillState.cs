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

        prevMode = _penguin.MyArmy.MovefocusMode;
        _penguin.MyArmy.MovefocusMode = MovefocusMode.Stop;
        _penguin.StopImmediately();

        _penguin.LookTargetImmediately();

        _triggerCalled = false;
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
        _penguin.MyArmy.MovefocusMode = prevMode;

        base.ExitState();
    }
}