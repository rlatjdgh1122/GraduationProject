using Unity.VisualScripting;
using UnityEngine;

public class ShieldBlockState : ShieldBaseState
{
    public ShieldBlockState(Penguin penguin, EntityStateMachine<ShieldPenguinStateEnum, Penguin> stateMachine, string animBoolName)
        : base(penguin, stateMachine, animBoolName)
    {
    }

    int StunAtk = 1;

    public override void Enter()
    {
        base.Enter();

        _penguin.WaitForCommandToArmyCalled  = false;
        _penguin.FindFirstNearestEnemy();
        _penguin.StopImmediately();

        /*foreach (var enemy in _penguin.FindNearestEnemy(5)) //일단 임시로 5마리도발 이것도 SO로 뺄거임
        {
            enemy.IsProvoked = true;
        }*/

        _penguin.HealthCompo.OnHit += ImpactShield;
    }

    public override void UpdateState()
    {
        base.UpdateState();

        _penguin.LookTarget();

        if (IsArmyCalledIn_CommandMode())
        {
            _stateMachine.ChangeState(ShieldPenguinStateEnum.MustMove);
        }
        else
        {
            //사거리가 멀어지면 맞으러 감
            if (!_penguin.IsInnerMeleeRange)
                _stateMachine.ChangeState(ShieldPenguinStateEnum.Chase);

            IsTargetNull(ShieldPenguinStateEnum.Idle);
        }


        if (StunAtk > 0 && _penguin.CheckStunEventPassive(_penguin.HealthCompo.maxHealth, _penguin.HealthCompo.currentHealth))
        {
            _penguin?.OnPassiveStunEvent();
            StunAtk--;
        }
    }

    private void ImpactShield()
    {
        _stateMachine.ChangeState(ShieldPenguinStateEnum.Impact);
    }

    public override void Exit()
    {
        _penguin.HealthCompo.OnHit -= ImpactShield;
        base.Exit();
    }
}
