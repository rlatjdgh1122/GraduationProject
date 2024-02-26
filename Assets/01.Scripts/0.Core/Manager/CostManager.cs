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

    private void Update() //�ӽ�
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            AddFromCurrentCost(6, true,transform);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            SubtractFromCurrentCost(6);
        }
    }

    public void SubtractFromCurrentCost(int price) //���� ��ȭ���� ����
    {
        _currentCost -= price;
        _costUI.ChangeCost(-Mathf.Abs(price));
    }

    //���� ��ȭ���� ���ϱ�
    //���� tween�� true�� �� �лл��ϴ°�,
    public void AddFromCurrentCost(int value, bool tween = false, Transform startTransform = null)
    {
        if(tween)
        {
            _costUI.CostTween(value, startTransform);
        }
        else 
        {
            _costUI.ChangeCost(value);
        }

        _currentCost += value;
    }
}
