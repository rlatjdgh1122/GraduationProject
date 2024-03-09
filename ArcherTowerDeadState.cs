using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTowerDeadState : ArcherTowerBaseState
{
    public ArcherTowerDeadState(Penguin penguin, EntityStateMachine<ArcherTowerPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }
}
