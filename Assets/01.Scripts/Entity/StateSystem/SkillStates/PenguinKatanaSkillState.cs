using ArmySystem;

public class PenguinKatanaSkillState : State
{
    private General General => _penguin as General;

    private MovefocusMode prevMode = MovefocusMode.Command;

    public PenguinKatanaSkillState(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _penguin.StopImmediately();
        _penguin.LookTargetImmediately();
        _penguin.MyArmy.MovefocusMode = MovefocusMode.Stop;
        //AttackEnter();

        _triggerCalled = false;
        General.skill.PlaySkill();
    }

    public override void UpdateState()
    {
        //base.UpdateState();

        if (_triggerCalled)
        {
            //_stateMachine.ChangeState(PenguinStateType.Chase);

            IsTargetNull(PenguinStateType.Idle);
        }
    }

    public override void ExitState()
    {
        _penguin.MyArmy.MovefocusMode = prevMode;

        base.ExitState();
    }
}