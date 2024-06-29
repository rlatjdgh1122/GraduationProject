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

    protected virtual void Awake() { }

    public void SetArmy(Army army)
    {
        myArmy = army;
    }

    public virtual void Init()
    {
        //½ºÅ³UI¶û ±Ã±Ø±âUI Init½ÃÄÑÁÜ

    }


}
