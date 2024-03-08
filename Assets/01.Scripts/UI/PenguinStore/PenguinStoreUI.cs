using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PenguinStorePanel
{
    public CanvasGroup panel;
    public float panelAlphaFadeTime;
}

public class PenguinStoreUI : MonoBehaviour
{
    public List<PenguinStorePanel> _panelList;

    [Header("PenguinStore")]
    [SerializeField] private Transform _spawnPenguinButtonParent;

    [Header("BuyPanel")]
    [SerializeField] private TextMeshProUGUI _buyCntText;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _currentCostText;
    [SerializeField] private TextMeshProUGUI _amountCostText;
    [SerializeField] private TextMeshProUGUI _buyToPenguinNameText;
    [SerializeField] private int _maxCount;
    private int _cnt = 1;
    private int _price = 0;

    [Header("PenguinInfoPanel")]
    [SerializeField] private TextMeshProUGUI _infoPenguinNameText;
    [SerializeField] private Image _penguinFace;
    [SerializeField] private Slider _rangeSlider;
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private Slider _atkSlider;

    private PenguinStat _stat;

    private SpawnPenguinButton[] _spawnPenguins;

    private void Awake()
    {
        _spawnPenguins = _spawnPenguinButtonParent.GetComponentsInChildren<SpawnPenguinButton>();
    }

    #region OnOffPanel

    public void OnEnableStorePanel() //����� �г� Ȱ��ȭ
    {
        _panelList[0].panel.DOFade(1, _panelList[0].panelAlphaFadeTime);
        _panelList[0].panel.blocksRaycasts = true;

        foreach(var btn in _spawnPenguins)
        {
            btn.SlotUpdate();
        }
    }

    public void OnDisableStorePanel()//����� �г� ��Ȱ��ȭ
    {
        _panelList[0].panel.DOFade(0, _panelList[0].panelAlphaFadeTime);
        _panelList[0].panel.blocksRaycasts = false;
    }
    public void OnEnableBuyPanel() //���� �г� Ȱ��ȭ
    {
        _panelList[1].panel.DOFade(1, _panelList[1].panelAlphaFadeTime);
        _panelList[1].panel.blocksRaycasts = true;

        CurrentCostUpdate();
        AmountCostUpdate();
    }

    public void OnDisableBuyPanel()//���� �г� ��Ȱ��ȭ
    {
        _panelList[1].panel.DOFade(0, _panelList[1].panelAlphaFadeTime);
        _panelList[1].panel.blocksRaycasts = false;

        ResetBuyPanel();
    }

    public void OnEnablePenguinInfo() //��� ���� Ȱ��ȭ
    {
        _panelList[2].panel.DOFade(1, _panelList[2].panelAlphaFadeTime);
        _panelList[2].panel.blocksRaycasts = true;

        UpdatePenguinInfo(_panelList[2].panelAlphaFadeTime);
    }

    public void OnDisablePenguinInfo() //��� ���� ��Ȱ��ȭ
    {
        _panelList[2].panel.DOFade(0, _panelList[2].panelAlphaFadeTime);
        _panelList[2].panel.blocksRaycasts = false;
    }

    #endregion

    #region BuyPanel

    private int _amountPrice;
    private void PriceUpdate() //����� �� ������ ����ؼ� ����
    {
        _amountPrice = (_price * _cnt);

        TextUpdate(_priceText, _amountPrice.ToString());

        _amountPrice = Mathf.Abs(_amountPrice);
    }

    private void CurrentCostUpdate() //���� ���� ��ȭ ����
    {
        _buyToPenguinNameText.text = _stat.PenguinName;
        TextUpdate(_currentCostText, CostManager.Instance.Cost.ToString());
    }

    public void PlusCnt() //UI �ȿ� �ִ� +��ư�� ������
    {
        if (_maxCount < _cnt) return;

        _cnt++;
        TextUpdate(_buyCntText, _cnt.ToString());
        AmountCostUpdate();
    }
    public void MinusCnt()//UI �ȿ� �ִ� -��ư�� ������
    {
        if (_cnt <= 1) return;

        _cnt--;
        TextUpdate(_buyCntText, _cnt.ToString());
        AmountCostUpdate();
    }

    private void AmountCostUpdate() //���� ���� ��ȭ���� �� ������ �� ���� ��ȭ ����
    {
        PriceUpdate();
        int amount = CostManager.Instance.Cost - _amountPrice;
        
        if(_amountPrice > CostManager.Instance.Cost) //���� �Ѱ����� ���� ��ȭ���� ũ�ٸ�
        {
            _amountCostText.color = Color.red;
            amount = -Mathf.Abs(amount);
        }
        else
        {
            _amountCostText.color = Color.green;
            amount = Mathf.Abs(amount);
        }


        TextUpdate(_amountCostText, amount.ToString());    
    }
    public void ResetBuyPanel()
    {
        _cnt = 1;
        _price = 0;
        _amountPrice = 0;
    }

    #endregion

    #region PenguinInfo

    private void UpdatePenguinInfo(float time)
    {
        _penguinFace.sprite = _stat.PenguinIcon;
        _infoPenguinNameText.text = _stat.PenguinName;

        _atkSlider.DOValue(_stat.PenguinData.atk, time);
        _hpSlider.DOValue(_stat.PenguinData.hp, time);
        _rangeSlider.DOValue(_stat.PenguinData.range, time);
    }


    #endregion

    public void PenguinInformataion(PenguinStat stat, int price) //���Կ��� ��� ���� �ޱ�
    {
        _price = -price; //����
        _stat = stat;


        PriceUpdate();
    }

    public void TextUpdate(TextMeshProUGUI text, string str)
    {
        text.text = str;
    }
}
