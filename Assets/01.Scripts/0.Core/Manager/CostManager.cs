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

    [SerializeField] private TextMeshProUGUI _curCostText;
    [SerializeField] private TextMeshProUGUI _plusCostText;

    [Header("Default Cost Value")]
    [SerializeField] private int _defaultCost;

    public override void Awake()
    {
        base.Awake();

        ChangeCost(_defaultCost);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            ChangeCost(-234);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            ChangeCost(123);
        }
    }

    public void ChangeCost(int cost)
    {
        _plusCostText.text = $"{cost}";
        _curCostText.text = $"{Cost}";

        _currentCost += cost;

        CostTween(cost);
    }

    public void CostTween(int cost)
    {
        UIManager.Instance.InitializeWarningTextSequence();

        UIManager.Instance.WarningTextSequence.AppendCallback(() =>
        {
            _plusCostText.alpha = 1;
            _plusCostText.enabled = true;

            UIManager.Instance.ChangeTextColorBoolean(
            _plusCostText,
            cost >= 0,
            Color.green,
            Color.red,
            0);

        }).Append(_plusCostText.DOFade(0, 1))
        .AppendInterval(0.2f)
        .AppendCallback(() =>
        {
            _plusCostText.enabled = false;
            _curCostText.text = $"{Cost}";
        });
    }
}
