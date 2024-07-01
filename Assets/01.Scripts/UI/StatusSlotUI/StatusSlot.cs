using ArmySystem;
using DG.Tweening;
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

    [SerializeField] protected SkillUI skillUI = null;
    [SerializeField] protected SkillUI UltimateUI = null;

    protected virtual void Awake() { }


    public virtual void Init()
    {
        //��ųUI�� �ñر�UI Init������
        skillUI.Init();
        UltimateUI.Init();
    }

    public void ChangedValue(ArmyUIInfo n)
    {
        //������ħ
        Debug.Log(n.ArmyName);
    }
}
