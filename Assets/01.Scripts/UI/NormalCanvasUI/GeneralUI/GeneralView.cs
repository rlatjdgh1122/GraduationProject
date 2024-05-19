using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class GeneralView : GeneralPopupUI, IPointerEnterHandler
{
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

    public override void Awake()
    {
        base.Awake();

        //���⼭�� Ŭ������
        GeneralInfoData = Instantiate(GeneralInfoData);
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
        stateText.text = "��ݵ�";
        purchaseText.text = $"{generalStat.InfoData.Price:N0} �����ϱ�";
    }

    public void SetUpgradeUI(GeneralStat generalStat)
    {
        purchaseButton.gameObject.SetActive(false);
        upgradeButton.gameObject.SetActive(true);
        icon.DOColor(Color.white, 0.6f);
        outline.DOColor(_outlineColor, 0.6f);
        levelText.gameObject.SetActive(true);
        stateText.text = "������";
        upgradeText.text = $"{generalStat.GeneralDetailData.levelUpPrice.GetValue():N0} ���׷��̵�";
        levelText.text = $"Lv {generalStat.GeneralDetailData.level}";
    }

    public void UpdateUpgradeUI(GeneralStat generalStat)
    {
        upgradeText.text = $"{generalStat.GeneralDetailData.levelUpPrice.GetValue():N0} ���׷��̵�";
        levelText.text = $"Lv {generalStat.GeneralDetailData.level}";
    }

    public void OnPurchase()
    {
        presenter.SetCurrentView(this); //�̰� �߰�����
        presenter.Purchase(GeneralInfoData);
    }

    public void OnUpgrade()
    {
        presenter.ShowUpgradePanel();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (presenter != null)
            presenter.SetCurrentView(this);
    }
}
