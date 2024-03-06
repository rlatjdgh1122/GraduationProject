using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTowerChaseState : ArcherTowerBaseState
{
    public ArcherTowerChaseState(Penguin penguin, EntityStateMachine<ArcherTowerPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }

}
