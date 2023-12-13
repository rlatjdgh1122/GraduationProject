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
        _penguin.FindNearestEnemy("Enemy");
        _penguin.owner.IsMoving = false;
        _penguin.StopImmediately();
        _penguin.AnimatorCompo.speed = _penguin.attackSpeed;
    }

    public override void UpdateState()
    {
        base.UpdateState();
        _penguin.LookTarget();

        if (_triggerCalled)
        {
            if (GameManager.Instance.GetEnemyPenguinCount() <= 0) //타겟이 없다면 가만히 있음
                _stateMachine.ChangeState(BasicPenguinStateEnum.Idle);

            _stateMachine.ChangeState(BasicPenguinStateEnum.Chase);
        }
    }

    public override void Exit()
    {
        _penguin.AnimatorCompo.speed = 1;
        _penguin.owner.IsMoving = true;
        base.Exit();
    }
}
