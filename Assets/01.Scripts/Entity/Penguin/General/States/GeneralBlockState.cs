using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralBlockState : GeneralBaseState
{
    public GeneralBlockState(General penguin, EntityStateMachine<GeneralPenguinStateEnum, General> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        base.Enter();
        _triggerCalled = true;
        _penguin.FindFirstNearestEnemy();
        _penguin.StopImmediately();

        _penguin.HealthCompo.OnHit += ImpactShield;
        //_penguin.skill.OnSkillFailed += SpinAttack;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _penguin.LookTarget();

        if (!_penguin.IsInnerMeleeRange)
            _stateMachine.ChangeState(GeneralPenguinStateEnum.Chase);

        if (_penguin.CurrentTarget == null)
            _stateMachine.ChangeState(GeneralPenguinStateEnum.Idle);
    }

    private void SpinAttack()
    {
        _stateMachine.ChangeState(GeneralPenguinStateEnum.SpinAttack);
    }

    private void ImpactShield()
    {
        _stateMachine.ChangeState(GeneralPenguinStateEnum.Impact);
    }

    public override void Exit()
    {
        _penguin.HealthCompo.OnHit -= ImpactShield;
        //_penguin.skill.OnSkillFailed -= SpinAttack;
        base.Exit();
    }
}
