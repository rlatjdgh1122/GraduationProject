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
            _costUI.OnlyCurrentCostView(value);
        }
    }

    [SerializeField] private SoundName _buySound = SoundName.Buy;

    [SerializeField] private CostUI _costUI;

    [Header("Default Cost Value")]
    [SerializeField] private int _defaultCost;

    public override void Awake()
    {
        base.Awake();

        Cost = _defaultCost;
        _costUI.OnlyCurrentCostView(Cost);
    }

    /// <summary>
    /// 현재 재화에서 가격을 빼주기
    /// </summary>
    /// <param name="price">가격</param>
    public void SubtractFromCurrentCost(int price) //현재 재화에서 빼기
    {
        SoundManager.Play2DSound(_buySound);

        _currentCost -= price;
        _costUI.SubtractCost(-Mathf.Abs(price));

    }

    /// <summary>
    /// 현재 재화에서 가격을 뺏을 때 재화가 남는지 여부
    /// </summary>
    /// <param name="price">가격</param>
    /// <returns></returns>
    public bool CheckRemainingCost(int price)
    {
        int remainCost = _currentCost - price;

        return remainCost >= 0 ? true : false;
    }

    /// <summary>
    /// 현재 재화에서 더하기
    /// </summary>
    /// <param name="value">더해질 값</param>
    /// <param name="tween">tween이 true면 돈 뿅뿅뿅하는거</param>
    /// <param name="isUI">UI가 아니면 false</param>
    /// <param name="startTransform">시작 위치</param>
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
