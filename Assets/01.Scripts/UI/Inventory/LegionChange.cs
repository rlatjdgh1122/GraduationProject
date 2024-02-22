using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

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

    [SerializeField] private CanvasGroup _clickByByttonImg;
    [SerializeField] private float _waitFadeOffTime;
    private TextMeshProUGUI _clickByByttonText;

    private int _curLegion;

    private int _finalCost;
    private bool _cantBuy;

    private void Awake()
    {
        _clickByByttonText = _clickByByttonImg.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        _legionButtons = _legionNumberTrm.GetComponentsInChildren<CanvasGroup>();
    }

    public void ChangeCurrentLegionNumber(int text)
    {
        _curLegionNumberTex.text = $"{text} ����";

        LegionInventory.Instance.ChangeLegion(text - 1);
    }

    public void SelectLegionNumber(int number) //���� ���� UI�� Ŭ������ ��
    {
        _curLegion = number;

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
        UIManager.Instance.InitializeWarningTextSequence();
        _backPanel.DOFade(0.5f, _changeTime);

        for (int i = 0; i < LegionInventory.Instance.LegionList.Count; i++)
        {
            _legionButtons[i].blocksRaycasts = true;

            UIManager.Instance.WarningTextSequence.Append(_legionButtons[i].DOFade(1, _changeTime));

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

        int curCost             = CostManager.Instance.CurrentCost;
        int price               = LegionInventory.Instance.LegionList[number].price;
        _finalCost           = curCost - price;

        if(_finalCost < 0)
        {
            _finalCostText.color = Color.red;
            _cantBuy = true;
        }
        else
        {
            _finalCostText.color = new Color(0.09f, 1f, 0f, 1f);
            _cantBuy = false;
        }

        _currentCostText.text   = $"{curCost}";
        _priceText.text         = $"{price}";
        _finalCostText.text     = $"{_finalCost}";
    }

    public void BuyBtn()
    {
        UIManager.Instance.InitializeWarningTextSequence();

        if (_cantBuy)
        {
            _clickByByttonText.text = "��ȭ�� �����մϴ�!";
        }
        else
        {
            ChangeCurrentLegionNumber(_curLegion);

            _clickByByttonText.text = $"{_curLegion}���� ���� ����!";

            CostManager.Instance.ChangeCost(_finalCost);
            LegionInventory.Instance.LegionList[_curLegion - 1].Locked = false;

            CloseBuyPanel();
        }

        UIManager.Instance.WarningTextSequence.Append(_clickByByttonImg.DOFade(1, _changeTime))
                .AppendInterval(_waitFadeOffTime)
                .Append(_clickByByttonImg.DOFade(0, _changeTime));
    }

    public void CloseBuyPanel()
    {
        _buyPanel.alpha = 0;
        _buyPanel.blocksRaycasts = false;

        SelectButton();
    }
}