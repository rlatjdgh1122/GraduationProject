using ArmySystem;
using DG.Tweening;
using SkillSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusSlot : MonoBehaviour, IValueChangeUnit<ArmyUIInfo>
{
    [SerializeField] protected Image synergyImage = null;
    [SerializeField] protected Image skillImage = null;
    [SerializeField] protected Image ultimateImage = null;

    protected DecisionType decision = DecisionType.None;
    protected Image skill

    protected virtual void Awake() { }

    public virtual void Init()
    {
        //½ºÅ³UI¶û ±Ã±Ø±âUI Init½ÃÄÑÁÜ
        skillUI.Init();
        UltimateUI.Init();
    }

    public virtual void ChangedValue(ArmyUIInfo newValue)
    {
        synergyImage.sprite = newValue.SynergySprite;
        skillImage.sprite = newValue.SkillSprite;
        ultimateImage.sprite = newValue.UltimateSprite;
    }
}
