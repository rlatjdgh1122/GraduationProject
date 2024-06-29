using ArmySystem;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusSlot : MonoBehaviour
{
    protected Army myArmy = null;

    [SerializeField] protected Image synergyImage  = null;
    [SerializeField] protected Image skillImage    = null;
    [SerializeField] protected Image ultimateImage = null;

    [SerializeField] protected SkillUI skillUI = null;
    [SerializeField] protected SkillUI UltimateUI = null;

    protected virtual void Awake() { }

    public void SetArmy(Army army)
    {
        myArmy = army;

        OnRegister();
    }

    private void OnRegister()
    {
        myArmy.OnArmyUIInfoChanged += OnUIInfoChangedHandler;
    }

   
    protected void OffRegister()
    {

    }
    private void OnUIInfoChangedHandler(ArmyUIInfo prevValue, ArmyUIInfo curValue)
    {
        
    }


    public virtual void Init()
    {
        //½ºÅ³UI¶û ±Ã±Ø±âUI Init½ÃÄÑÁÜ
        skillUI.Init();
        UltimateUI.Init();
    }


}
