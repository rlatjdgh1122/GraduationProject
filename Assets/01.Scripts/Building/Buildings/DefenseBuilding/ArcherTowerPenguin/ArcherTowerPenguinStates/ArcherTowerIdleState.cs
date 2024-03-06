using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTowerIdleState : ArcherTowerBaseState
{
    public ArcherTowerIdleState(Penguin penguin, EntityStateMachine<ArcherTowerPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }
}
