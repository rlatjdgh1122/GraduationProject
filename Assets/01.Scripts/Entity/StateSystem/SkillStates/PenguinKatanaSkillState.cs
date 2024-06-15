public class PenguinKatanaSkillState : State
{
    private General General => _penguin as General;

    public PenguinKatanaSkillState(Penguin penguin, PenguinStateMachine stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _penguin.LookTargetImmediately();
        AttackEnter();

        _triggerCalled = false;
        General.skill.PlaySkill();
    }

    public override void UpdateState()
    {
        //base.UpdateState();

        if (_triggerCalled)
        {
            _stateMachine.ChangeState(PenguinStateType.Chase);

            IsTargetNull(PenguinStateType.Idle);
        }
    }

    public override void ExitState()
    {
        AttackExit();

        base.ExitState();
    }
}