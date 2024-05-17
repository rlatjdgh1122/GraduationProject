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
    Miner,
    Builder,
    ShieldGeneral,
    KatanaGeneral,
    Wizard,
    SumoGeneral,
}

public class PenguinSpawner : DefaultBuilding
{
    protected override void Start()
    {
        base.Start();
        SignalHub.OnTutorialArrowSignEvent?.Invoke(transform);
        UIManager.Instance.HidePanel("Masking");
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
