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
    /// ���� ��ȭ���� ������ ���ֱ�
    /// </summary>
    /// <param name="price">����</param>
    public void SubtractFromCurrentCost(float price, Action onSuccesAction = null) //���� ��ȭ���� ����
    {
        if (!CheckRemainingCost(price))
        {
            UIManager.Instance.ShowWarningUI("��ȭ�� �����մϴ�.");
            return;
        }

        SoundManager.Play2DSound(_buySound);

        _currentCost -= price;
        _costUI.ChangeCost();

        onSuccesAction?.Invoke();
    }

    /// <summary>
    /// ���� ��ȭ���� ������ ���� �� ��ȭ�� ������ ����
    /// </summary>
    /// <param name="price">����</param>
    /// <returns></returns>
    public bool CheckRemainingCost(float price)
    {
        float remainCost = _currentCost - price;

        return remainCost >= 0 ? true : false;
    }

    /// <summary>
    /// ���� ��ȭ���� ���ϱ�
    /// </summary>
    /// <param name="value">������ ��</param>
    /// <param name="tween">tween�� true�� �� �лл��ϴ°�</param>
    /// <param name="isUI">UI�� �ƴϸ� false</param>
    /// <param name="startTransform">���� ��ġ</param>
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
