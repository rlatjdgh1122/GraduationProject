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

    public void OnEnableStorePanel() //스토어 패널 활성화
    {
        _panelList[0].panel.DOFade(1, _panelList[0].panelAlphaFadeTime);
        _panelList[0].panel.blocksRaycasts = true;

        foreach(var btn in _spawnPenguins)
        {
            btn.SlotUpdate();
        }
    }

    public void OnDisableStorePanel()//스토어 패널 비활성화
    {
        _panelList[0].panel.DOFade(0, _panelList[0].panelAlphaFadeTime);
        _panelList[0].panel.blocksRaycasts = false;
    }
    public void OnEnableBuyPanel() //구매 패널 활성화
    {
        _panelList[1].panel.DOFade(1, _panelList[1].panelAlphaFadeTime);
        _panelList[1].panel.blocksRaycasts = true;

        CurrentCostUpdate();
        AmountCostUpdate();
    }

    public void OnDisableBuyPanel()//구매 패널 비활성화
    {
        _panelList[1].panel.DOFade(0, _panelList[1].panelAlphaFadeTime);
        _panelList[1].panel.blocksRaycasts = false;

        ResetBuyPanel();
    }

    public void OnEnablePenguinInfo() //펭귄 정보 활성화
    {
        _panelList[2].panel.DOFade(1, _panelList[2].panelAlphaFadeTime);
        _panelList[2].panel.blocksRaycasts = true;

        UpdatePenguinInfo(_panelList[2].panelAlphaFadeTime);
    }

    public void OnDisablePenguinInfo() //펭귄 정보 비활성화
    {
        _panelList[2].panel.DOFade(0, _panelList[2].panelAlphaFadeTime);
        _panelList[2].panel.blocksRaycasts = false;
    }

    #endregion

    #region BuyPanel

    private int _amountPrice;
    private void PriceUpdate() //펭귄의 총 가격을 계산해서 업뎃
    {
        _amountPrice = (_price * _cnt);

        TextUpdate(_priceText, _amountPrice.ToString());

        _amountPrice = Mathf.Abs(_amountPrice);
    }

    private void CurrentCostUpdate() //현재 보유 재화 업뎃
    {
        _buyToPenguinNameText.text = _stat.PenguinName;
        TextUpdate(_currentCostText, CostManager.Instance.Cost.ToString());
    }

    public void PlusCnt() //UI 안에 있는 +버튼을 누르면
    {
        if (_maxCount < _cnt) return;

        _cnt++;
        TextUpdate(_buyCntText, _cnt.ToString());
        AmountCostUpdate();
    }
    public void MinusCnt()//UI 안에 있는 -버튼을 누르면
    {
        if (_cnt <= 1) return;

        _cnt--;
        TextUpdate(_buyCntText, _cnt.ToString());
        AmountCostUpdate();
    }

    private void AmountCostUpdate() //현재 보유 재화에서 총 가격을 뺀 남은 재화 업뎃
    {
        PriceUpdate();
        int amount = CostManager.Instance.Cost - _amountPrice;
        
        if(_amountPrice > CostManager.Instance.Cost) //만약 총가격이 현재 재화보다 크다면
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

    public void PenguinInformataion(PenguinStat stat, int price) //슬롯에서 펭귄 정보 받기
    {
        _price = -price; //가격
        _stat = stat;


        PriceUpdate();
    }

    public void TextUpdate(TextMeshProUGUI text, string str)
    {
        text.text = str;
    }
}
