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
    private TextMeshProUGUI _remainCostText;
    private TextMeshProUGUI _healPercentText;

    [SerializeField]
    private TextMeshProUGUI _situationText, _penguinNameText;

    private float _hpPercent = 0;
    private int _penguinPrce = 0;

    public override void Awake()
    {
        base.Awake();

        _remainCostText   = _healPanel.transform.Find("RemainCostText").GetComponent<TextMeshProUGUI>();
        _healPercentText  = _healPanel.transform.Find("CurrentPenguinHPText").GetComponent<TextMeshProUGUI>();
    }
    public void SetPenguinSituation(EntityInfoDataSO so, float percent, int penguinPrice)
    {
        if (so.LegionName == "�Ҽӵ� ���� ����")
        {
            UIManager.Instance.ShowWarningUI("������ �������ּ���!");
            return;
        }

        data = so;
        _hpPercent = percent;
        _penguinPrce = penguinPrice;
        _penguinNameText.text = so.PenguinName;

        ShowPanel();
        ShowHealPanel();
    }

    public void ShowHealPanel()
    {
        CheckBuy(_hpPercent, _penguinPrce);

        int remainCost = price;

        ChangingPanel(_retirePanel, _healPanel);

        isHeal   = true;
        isRetire = false;


        string sign;
        Color color;

        if (CostManager.Instance.CheckRemainingCost(price))
        {
            color = Color.green;
            sign = "";
        }
        else
        {
            color = Color.red;
            sign = "-";
        }

        string arrow = isReducedHP ? "<color=red>��</color>" : null;

        ChangeTextColor(_remainCostText, color);

        _remainCostText.text   = $"���� ��ȭ    :    {sign}{CostManager.Instance.Cost - price}";
        _healPercentText.text = $"���� HP {hpPercent}% {arrow}";
        _situationText.text   = $"��� ȸ���ϱ� (����: {remainCost})";
    }

    public void ShowRetirePanel()
    {
        ChangingPanel(_healPanel, _retirePanel);

        isHeal   = false;
        isRetire = true;
        canClick  = true;

        _situationText.text = "��� �����ϱ�";
    }

    private void ChangingPanel(CanvasGroup beforePanel, CanvasGroup afterPanel)
    {
        beforePanel.alpha = 0;
        beforePanel.blocksRaycasts = false;

        afterPanel.alpha  = 1;
        afterPanel.blocksRaycasts  = true;
    }

    private void ChangeTextColor(TextMeshProUGUI text,  Color color)
    {
        text.color = color;
    }
}