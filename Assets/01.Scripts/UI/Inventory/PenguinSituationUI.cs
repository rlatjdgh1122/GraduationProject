using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class PenguinSituationUI : PenguinSituationPanel
{
    [SerializeField]
    private CanvasGroup _healPanel, _retirePanel;
    private TextMeshProUGUI _healPriceText;
    private TextMeshProUGUI _healPercentText;

    [SerializeField]
    private TextMeshProUGUI _situationText, _penguinNameText;

    public override void Awake()
    {
        base.Awake();

        _healPriceText   = _healPanel.transform.Find("PriceText").GetComponent<TextMeshProUGUI>();
        _healPercentText = _healPanel.transform.Find("CurrentPenguinHP").GetComponent<TextMeshProUGUI>();
    }
    public void SetPenguinSituation(EntityInfoDataSO so, float percent, int penguinPrice)
    {
        if (so.LegionName == "소속된 군단 없음")
        {
            UIManager.Instance.ShowWarningUI("군단을 저장해주세요!");
            return;
        }

        data = so;

        CheckBuy(percent, penguinPrice);

        ShowPanel();
        ShowHealPanel();

        _penguinNameText.text = so.PenguinName;
    }

    public void ShowHealPanel()
    {
        int remainCost = CostManager.Instance.Cost - price;

        ChangingPanel(_retirePanel, _healPanel);

        canHeal   = true;
        canRetire = false;

        _healPriceText.text   = $"가격     :    -{price}";
        _healPercentText.text = $"현재 HP {hpPercent}%";
        _situationText.text   = $"펭귄 회복하기 (남는 재화: {remainCost})";
    }

    public void ShowRetirePanel()
    {
        ChangingPanel(_healPanel, _retirePanel);

        canHeal   = false;
        canRetire = true;

        canClick  = true;

        _situationText.text = "펭귄 은퇴하기";
    }

    private void ChangingPanel(CanvasGroup beforePanel, CanvasGroup afterPanel)
    {
        beforePanel.alpha = 0;
        beforePanel.blocksRaycasts = false;

        afterPanel.alpha  = 1;
        afterPanel.blocksRaycasts  = true;
    }
}