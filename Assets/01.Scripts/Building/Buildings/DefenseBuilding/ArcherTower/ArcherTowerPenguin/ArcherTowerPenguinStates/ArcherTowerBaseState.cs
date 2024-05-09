using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTowerBaseState : EntityState<ArcherTowerPenguinStateEnum, Penguin>
{
    public ArcherTowerBaseState(Penguin penguin, EntityStateMachine<ArcherTowerPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
