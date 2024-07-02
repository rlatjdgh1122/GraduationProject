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
    private Army _army = null;

    [SerializeField] protected Image synergyIcon = null;
    [SerializeField] protected Image skillIcon = null;
    [SerializeField] protected Image ultimateIcon = null;

    [SerializeField] private GameObject _skillUIObj = null;
    [SerializeField] private GameObject _ultimateUIObj = null;

    protected SkillUI skillUI = null;
    protected UltimateUI ultimateUI = null;

    protected DecisionType decision = DecisionType.None;

    protected virtual void Awake() { }

    protected void OnDisable()
    {
        OffRegister();
    }

    private void OffRegister()
    {
        if (skillUI != null)
        {
            _army.SkillController.OnSkillUsedEvent -= skillUI.OnSkillUsed;
            _army.SkillController.OnChangedMaxValueEvent -= skillUI.OnChangedMaxValue;
            _army.SkillController.OnSkillActionEnterEvent -= skillUI.OnSkillActionEnter;
        }

        if (ultimateUI != null)
        {
            _army.UltimateController.OnSkillUsedEvent -= ultimateUI.OnUltimateUsed;
            _army.UltimateController.OnChangedMaxValueEvent -= ultimateUI.OnChangedMaxValue;
            _army.UltimateController.OnSkillActionEnterEvent -= ultimateUI.OnUltimateActionEnter;
        }
    }

    public void OnRegister()
    {
        if (skillUI != null)
        {
            _army.SkillController.OnSkillUsedEvent += skillUI.OnSkillUsed;
            _army.SkillController.OnChangedMaxValueEvent += skillUI.OnChangedMaxValue;
            _army.SkillController.OnSkillActionEnterEvent += skillUI.OnSkillActionEnter;
        }

        if (ultimateUI != null)
        {
            _army.UltimateController.OnSkillUsedEvent += ultimateUI.OnUltimateUsed;
            _army.UltimateController.OnChangedMaxValueEvent += ultimateUI.OnChangedMaxValue;
            _army.UltimateController.OnSkillActionEnterEvent += ultimateUI.OnUltimateActionEnter;
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
        ultimateUI = compo as UltimateUI;
    }


    public virtual void SetSkillUI(SkillType type, Sprite image)
    {
        //장군 있을때만 실행됨
        skillIcon.sprite = image;

        //디시젼타입에 따라 리플랙션
        string typeName = type.ToString();
        Type t = Type.GetType($"{typeName}SkillUI");
        Component compo = _skillUIObj.AddComponent(t);
        skillUI = compo as SkillUI;
    }

    public virtual void Init()
    {
        //스킬UI랑 궁극기UI Init시켜줌
        skillUI?.Init();
        ultimateUI?.Init();
    }

    public virtual void ChangedValue(ArmyUIInfo newValue)
    {
        //지금은 안쓰고 나중에 리펙토링할때 쓸듯
    }
}
