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
    public void SubtractFromCurrentCost(int price) //���� ��ȭ���� ����
    {
        SoundManager.Play2DSound(_buySound);

        _currentCost -= price;
        _costUI.SubtractCost(-Mathf.Abs(price));

    }

    public bool CheckRemainingCost(int price)
    {
        int remainCost = _currentCost - price;

        return remainCost >= 0 ? true : false;
    }

    //���� ��ȭ���� ���ϱ�
    //���� tween�� true�� �� �лл��ϴ°�,
    //UI�� �ƴϸ� false
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
