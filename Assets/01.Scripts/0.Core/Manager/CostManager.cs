using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class CostManager : Singleton<CostManager>
{
    private int _currentCost;

    public int Cost
    {
        get
        {
            return _currentCost;
        }
        set
        {
            _currentCost = value;
        }
    }

    [SerializeField] private CostUI _costUI;

    [Header("Default Cost Value")]
    [SerializeField] private int _defaultCost;

    public override void Awake()
    {
        base.Awake();

        Cost = _defaultCost;
        _costUI.OnlyCurrentCostView(Cost);
    }
    public void SubtractFromCurrentCost(int price) //현재 재화에서 빼기
    {
        _currentCost -= price;
        _costUI.SubtractCost(-Mathf.Abs(price));
    }

    //현재 재화에서 더하기
    //만약 tween이 true면 돈 뿅뿅뿅하는거,
    //UI가 아니면 false
    public void AddFromCurrentCost(int value, bool tween = false, bool isUI = false, Vector3 startTransform = new())
    {
        if(tween)
        {
            _costUI.CostTween(value, isUI, startTransform);
        }
        else 
        {
            _costUI.AddCost(value);
        }
    }

    public void OnlyCostUIUseThis(int value)
    {
        _currentCost += value;
    }

    public void CostArriveText(int cost)
    {
        _costUI.CostArriveText(cost);
    } 
    
    public void CostStopText()
    {
        _costUI.CostStopText();
    }
}
