using DG.Tweening;
using System;
using UnityEngine;

public class CostManager : Singleton<CostManager>
{
    private float _currentCost;

    public float Cost => _currentCost;

    [SerializeField] private SoundName _buySound = SoundName.Buy;

    private CostUI _costUI;

    [Header("Default Cost Value")]
    [SerializeField] private int _defaultCost;

    public override void Awake()
    {
        base.Awake();

        _costUI = FindObjectOfType<CostUI>();

        AddFromCurrentCost(_defaultCost, false, true);
    }

    /// <summary>
    /// 현재 재화에서 가격을 빼주기
    /// </summary>
    /// <param name="price">가격</param>
    public void SubtractFromCurrentCost(float price, Action onSuccesAction = null) //현재 재화에서 빼기
    {
        if (!CheckRemainingCost(price))
        {
            UIManager.Instance.ShowWarningUI("재화가 부족합니다.");
            return;
        }

        SoundManager.Play2DSound(_buySound);

        _currentCost -= price;
        _costUI.ChangeCost();

        onSuccesAction?.Invoke();
    }

    /// <summary>
    /// 현재 재화에서 가격을 뺏을 때 재화가 남는지 여부
    /// </summary>
    /// <param name="price">가격</param>
    /// <returns></returns>
    public bool CheckRemainingCost(float price)
    {
        float remainCost = _currentCost - price;

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
            AddCost(value);
            _costUI.ChangeCost();
        }
    }

    public void AddCost(int value)
    {
        _currentCost += value;
    }

    public void CostArriveText(float cost)
    {
        _costUI.CostArriveText(cost);
    } 
    
    public void CostStopText()
    {
        _costUI.CostStopText();
    }
}
