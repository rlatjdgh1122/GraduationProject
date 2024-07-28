using ArmySystem;
using DG.Tweening;
using SkillSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.UI;

public class StatusSlot : MonoBehaviour, IValueChangeUnit<ArmyUIInfo>
{
    private Army _army = null;

    [SerializeField] protected Image synergyIcon = null;
    [SerializeField] protected Image skillIcon = null;
    [SerializeField] protected Image ultimateIcon = null;

    [SerializeField] private GameObject _skillUIObj = null;
    [SerializeField] private GameObject _ultimateUIObj = null;

    public SkillUI SkillUI = null;
    public UltimateUI UltimateUI = null;

    protected DecisionType decision = DecisionType.None;

    protected virtual void Awake() { }

    protected void OnDisable()
    {
        OffRegister();
    }

    private void OffRegister()
    {
        if (SkillUI != null && _army.SkillController)
        {
            _army.SkillController.OnSkillUsedEvent -= SkillUI.OnSkillUsed;
            _army.SkillController.OnChangedMaxValueEvent -= SkillUI.OnChangedMaxValue;
            _army.SkillController.OnSkillActionEnterEvent -= SkillUI.OnSkillActionEnter;
            _army.SkillController.OnSkillReadyEvent -= SkillUI.OnSkillReady;
        }

        if (UltimateUI != null && _army.UltimateController)
        {
            _army.UltimateController.OnSkillUsedEvent -= UltimateUI.OnUltimateUsed;
            _army.UltimateController.OnChangedMaxValueEvent -= UltimateUI.OnChangedMaxValue;
            _army.UltimateController.OnSkillActionEnterEvent -= UltimateUI.OnUltimateActionEnter;
            //_army.UltimateController.OnSkillReadyEvent -= UltimateUI.OnSkillReady;
        }
    }

    public void OnRegister()
    {
        if (SkillUI != null && _army.SkillController)
        {
            _army.SkillController.OnSkillUsedEvent += SkillUI.OnSkillUsed;
            _army.SkillController.OnChangedMaxValueEvent += SkillUI.OnChangedMaxValue;
            _army.SkillController.OnSkillActionEnterEvent += SkillUI.OnSkillActionEnter;
            _army.SkillController.OnSkillReadyEvent += SkillUI.OnSkillReady;

            _army.SkillController.Init();
        }

        if (UltimateUI != null && _army.UltimateController)
        {
            _army.UltimateController.OnSkillUsedEvent += UltimateUI.OnUltimateUsed;
            _army.UltimateController.OnChangedMaxValueEvent += UltimateUI.OnChangedMaxValue;
            _army.UltimateController.OnSkillActionEnterEvent += UltimateUI.OnUltimateActionEnter;

            _army.UltimateController.Init();
        }
    }

    public virtual void SetArmy(Army army)
    {
        _army = army;

        OnRegister();
    }

    public virtual void SetSynergyUI(Sprite image)
    {
        synergyIcon.sprite = image;
    }

    public virtual void SetUltimateUI(UltimateType type, Sprite image)
    {
        ultimateIcon.sprite = image;

        //디시젼타입에 따라 리플랙션
        string typeName = type.ToString();
        Type t = Type.GetType($"{typeName}UltimateUI");
        Component compo = _ultimateUIObj.AddComponent(t);
        UltimateUI = compo as UltimateUI;
    }


    public virtual void SetSkillUI(SkillType type, Sprite image)
    {
        //장군 있을때만 실행됨
        skillIcon.sprite = image;

        //디시젼타입에 따라 리플랙션
        string typeName = type.ToString();
        Type t = Type.GetType($"{typeName}SkillUI");
        Component compo = _skillUIObj.AddComponent(t);
        SkillUI = compo as SkillUI;
    }

    public virtual void Init()
    {
        //스킬UI랑 궁극기UI Init시켜줌
        SkillUI?.Init();
        UltimateUI?.Init();
    }

    public virtual void ChangedValue(ArmyUIInfo newValue)
    {
        //지금은 안쓰고 나중에 리펙토링할때 쓸듯
    }
}
