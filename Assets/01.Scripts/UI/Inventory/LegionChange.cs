using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Experimental.GlobalIllumination;

public class LegionChange : MonoBehaviour
{

    [SerializeField] private float _changeTime = 0.2f;

    [SerializeField] private TextMeshProUGUI _curLegionNumberTex;

    [SerializeField] private Image _backPanel;
    [SerializeField] private Transform _legionNumberTrm;

    private CanvasGroup[] _legionButtons;

    [Header("Buy Legion UI")]
    [SerializeField] private CanvasGroup _buyPanel;
    [SerializeField] private TextMeshProUGUI _currentCostText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _finalCostText;

    [SerializeField] private float _waitFadeOffTime;

    private int legion = 0;

    private int prevLegion = 0;

    public int Legion
    {
        get
        {
            return legion;
        }
        set
        {
            prevLegion = legion;
            legion = value;
        }
    }
    private int _curCost;
    private int _price;
    private int _finalCost;
    private bool _cantBuy;

    private void Start()
    {
        _legionButtons = _legionNumberTrm.GetComponentsInChildren<CanvasGroup>();
    }

    public void ChangeCurrentLegionNumber(int text)
    {
        _curLegionNumberTex.text = $"{text} ����";

        LegionInventory.Instance.ChangeLegion(text - 1);
        LegionInventory.Instance.LegionCountInformation(text - 1);

        SignalHub.OnUILegionChanged?.Invoke(prevLegion, Legion);
    }

    public void SelectLegionNumber(int number) //���� ���� UI�� Ŭ������ ��
    {
        Legion = number;

        if (LegionInventory.Instance.LegionList[number - 1].Locked) //���� Ŭ���� ������ ���������
        {
            BuyLegion(number - 1);
        }
        else
        {
            ChangeCurrentLegionNumber(number);

            SelectButton();
        }
    }

    public void ChangeButton() //���� �ٲٴ� ��ư�� ������ ��
    {
        UIManager.Instance.InitializHudTextSequence();
        _backPanel.DOFade(0.5f, _changeTime);

        for (int i = 0; i < LegionInventory.Instance.LegionList.Count; i++)
        {
            _legionButtons[i].blocksRaycasts = true;

            UIManager.Instance.HudTextSequence.Append(_legionButtons[i].DOFade(1, _changeTime));

            Image obj = _legionButtons[i].transform.Find("Locked").GetComponent<Image>(); //LegionBtn ������Ʈ �ؿ� �ִ�
            //Locked ������Ʈ ã��

            if (LegionInventory.Instance.LegionList[i].Locked) //������ ���������
            {
                obj.gameObject.SetActive(true);
            }
            else
            {
                obj.gameObject.SetActive(false);
            }
        }
    }

    private void SelectButton() //���� ���� ��ư
    {
        _backPanel.DOFade(0f, _changeTime);

        for (int i = _legionButtons.Length - 1; i >= 0; i--)
        {
            _legionButtons[i].DOFade(0, _changeTime);
            _legionButtons[i].blocksRaycasts = false;
        }
    }

    private void BuyLegion(int number)
    {
        _buyPanel.alpha = 1;
        _buyPanel.blocksRaycasts = true;

        _curCost = CostManager.Instance.Cost;
        _price = LegionInventory.Instance.LegionList[number].price;
        _finalCost = _curCost - _price;

        if (_finalCost < 0)
        {
            _finalCostText.color = Color.red;
            _cantBuy = true;
        }
        else
        {
            _finalCostText.color = new Color(0.09f, 1f, 0f, 1f);
            _cantBuy = false;
        }

        _currentCostText.text = $"{_curCost}";
        _priceText.text = $"-{_price}";
        _finalCostText.text = $"{_finalCost}";
    }

    public void BuyBtn()
    {
        UIManager.Instance.InitializHudTextSequence();

        if (_cantBuy)
        {
            LegionInventory.Instance.ShowMessage("��ȭ�� �����մϴ�!");
        }
        else
        {
            ChangeCurrentLegionNumber(Legion);

            LegionInventory.Instance.ShowMessage($"{Legion}���� ���� ����!");
            ArmyManager.Instance.CreateArmy();
            CostManager.Instance.SubtractFromCurrentCost(_price);
            LegionInventory.Instance.LegionList[Legion - 1].Locked = false;

            CloseBuyPanel();
        }
    }

    public void CloseBuyPanel()
    {
        _buyPanel.alpha = 0;
        _buyPanel.blocksRaycasts = false;

        SelectButton();
    }
}