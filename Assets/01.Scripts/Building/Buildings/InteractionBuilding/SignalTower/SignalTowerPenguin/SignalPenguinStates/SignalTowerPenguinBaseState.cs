using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalTowerPenguinBaseState : EntityState<SignalTowerPenguinStateEnum, Penguin>
{
    public SignalTowerPenguinBaseState(Penguin penguin, EntityStateMachine<SignalTowerPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateState()
    {
        base.UpdateState();

        if (_penguin.IsDead)
            _stateMachine.ChangeState(SignalTowerPenguinStateEnum.Dead);
    }

    public override void Exit()
    {
        base.Exit();
    }
}