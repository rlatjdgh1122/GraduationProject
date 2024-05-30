using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ArmyInfoPanel : PopupUI
{
    [Header("UI Elements")]
    [SerializeField] private Image _generalIcon;
    [SerializeField] private TextMeshProUGUI _generalNameText;
    [SerializeField] private Button _purchaseButton;

    [Header("Child Components")]
    public GeneralInfoPanel GeneralInfoPanel;

    public override void Awake()
    {
        base.Awake();
    }

    //장군 첫 구매에 대한 세팅 함수
    public void SetPurchaseInfo(GeneralStat data, DummyPenguinFactory factory, GeneralDummyPengiun dummy)
    {
        _generalIcon.sprite = data.InfoData.PenguinIcon;
        _generalNameText.text = data.InfoData.PenguinName;

        GeneralInfo generalInfo = new GeneralInfo(data, factory, dummy);
        _purchaseButton.onClick.AddListener(generalInfo.OnPurchase);

        ShowPanel();
        MovePanel(340, 0, 0.65f, false);
        GeneralInfoPanel.ShowPanel();
        GeneralInfoPanel.SetElements(data);
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }

    public override void MovePanel(float x, float y, float fadeTime, bool ease = true)
    {
        base.MovePanel(x, y, fadeTime, ease);
    }
}
