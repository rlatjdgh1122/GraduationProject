using ArmySystem;
using Unity.VisualScripting;
using UnityEngine;

public class ShieldBlockState : ShieldBaseState
{
    private int _currentBlockCnt = 0;

    public ShieldBlockState(Penguin penguin, EntityStateMachine<ShieldPenguinStateEnum, Penguin> stateMachine, string animBoolName)
        : base(penguin, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _currentBlockCnt++;
        _penguin.WaitForCommandToArmyCalled = false;
        _penguin.StopImmediately();
        _penguin.FindNearestEnemyInTargetArmy();

        if (_penguin.CurrentTarget != null)
            _penguin.CurrentTarget.HealthCompo.OnDied += DeadTarget;
        else
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Idle);

        _penguin.HealthCompo.OnHit += ImpactShield;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _penguin.LookTarget();

        //사거리가 멀어지면 맞으러 감
        if (!_penguin.IsTargetInAttackRange)
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Chase);

        IsTargetNull(ShieldPenguinStateEnum.Idle);

        CheckCommandModeForChase();
        CheckCommandModeForMovement();

    }

    private void ImpactShield()
    {
        if (_currentBlockCnt >= _penguin.MaxImapactCount)
        {
            _penguin?.OnPassiveHealthRatioEvent();
            _currentBlockCnt = 0;
        }
        else
        {
            _stateMachine.ChangeState(ShieldPenguinStateEnum.Impact);
        }
    }

    public override void Exit()
    {
        if (_penguin.CurrentTarget != null)
            _penguin.CurrentTarget.HealthCompo.OnDied -= DeadTarget;

        _penguin.HealthCompo.OnHit -= ImpactShield;
        base.Exit();
    }
}
