using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherTowerAttackState : ArcherTowerBaseState
{
    public ArcherTowerAttackState(Penguin penguin, EntityStateMachine<ArcherTowerPenguinStateEnum, Penguin> stateMachine, string animationBoolName) : base(penguin, stateMachine, animationBoolName)
    {
    }
}
