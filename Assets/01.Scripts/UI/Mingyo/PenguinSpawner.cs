using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public enum PenguinTypeEnum
{
    Basic,
    Archer,
    Shield,
    Mop,
    Sign,
}

public class PenguinSpawner : DefaultBuilding
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Running()
    {
        base.Running();
    }

    public override void UpdateSpawnUIBool()
    {
        base.UpdateSpawnUIBool();
    }
}
