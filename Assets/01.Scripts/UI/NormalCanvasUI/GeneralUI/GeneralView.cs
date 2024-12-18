using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class GeneralView : GeneralPopupUI
{
    [SerializeField]
    private GeneralStat _generalInfo;

    [HideInInspector]
    public GeneralStat GeneralInfoData;
    public GeneralDummyPengiun dummyGeneralPenguin;

    [SerializeField] private Color _outlineColor;

    [Header("UI Elements")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI purchaseText;
    public TextMeshProUGUI upgradeText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI stateText;
    public Image icon;
    public Image outline;
    public Button purchaseButton;
    public Button upgradeButton;

    private void OnDisable()
    {
        GeneralInfoData.InfoData.Setting();
    }

    private void Start()
    {
        //여기서도 클론으로
        GeneralInfoData = Instantiate(_generalInfo);
        PenguinManager.Instance.AddGeneralStat(GeneralInfoData.InfoData.PenguinType, GeneralInfoData);

        if (GeneralInfoData.GeneralDetailData.IsAvailable)
            SetUpgradeUI(GeneralInfoData);
        else
            UpdateDefaultUI(GeneralInfoData);
    }


    public void UpdateDefaultUI(GeneralStat generalStat)
    {
        nameText.text = generalStat.InfoData.PenguinName;
        icon.sprite = generalStat.InfoData.PenguinIcon;
        stateText.text = "잠금됨";
        purchaseText.text = $"{generalStat.InfoData.Price:N0} 영입하기";
    }

    public void SetUpgradeUI(GeneralStat generalStat)
    {
        Debug.Log("셋업");
        Debug.Log($"{generalStat.name}");
        Debug.Log($"{generalStat.GeneralDetailData.levelUpPrice.GetValue():N0} 업그레이드");
        Debug.Log($"{generalStat.GeneralDetailData.level} 업그레이드");
        purchaseButton.gameObject.SetActive(false);
        upgradeButton.gameObject.SetActive(true);
        icon.DOColor(Color.white, 0.6f);
        outline.DOColor(_outlineColor, 0.6f);
        levelText.gameObject.SetActive(true);
        stateText.text = "보유중";
        upgradeText.text = $"{generalStat.GeneralDetailData.levelUpPrice.GetValue():N0} 업그레이드";
        levelText.text = $"Lv {generalStat.GeneralDetailData.level}";
    }

    public void UpdateUpgradeUI(GeneralStat generalStat)
    {
        upgradeText.text = $"{generalStat.GeneralDetailData.levelUpPrice.GetValue():N0} 업그레이드";
        levelText.text = $"Lv {generalStat.GeneralDetailData.level}";
    }

    public void OnPurchase()
    {
        Debug.Log("눌리긴함");
        presenter.SetCurrentView(this); //이거 추가했음
        Debug.Log(GeneralInfoData.name);
        presenter.Purchase(GeneralInfoData);
    }

    public void OnUpgrade()
    {
        presenter.ShowUpgradePanel();
    }

  /*  public void OnPointerEnter(PointerEventData eventData)
    {
        if (presenter != null)
            presenter.SetCurrentView(this);
    }*/
}
