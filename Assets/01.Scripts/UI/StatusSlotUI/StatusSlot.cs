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
    [SerializeField] protected Image synergyIcon = null;
    [SerializeField] protected Image skillIcon = null;
    [SerializeField] protected Image ultimateIcon = null;

    protected DecisionType decision = DecisionType.None;
    protected Image skillImage = null;

    protected virtual void Awake() { }

    public virtual void Init()
    {
        //½ºÅ³UI¶û ±Ã±Ø±âUI Init½ÃÄÑÁÜ
    }

    public virtual void ChangedValue(ArmyUIInfo newValue)
    {
        synergyIcon.sprite = newValue.SynergySprite;
        skillImage.sprite = newValue.SkillSprite;
        ultimateIcon.sprite = newValue.UltimateSprite;
    }
}
