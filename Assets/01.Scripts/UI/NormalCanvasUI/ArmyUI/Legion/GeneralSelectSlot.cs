using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneralSelectSlot : ArmyComponentUI
{
    public GeneralStat GeneralData;
    private Button _purchaseButton;
    private Image _icon;
    private Image _lockedPanel;
    private TextMeshProUGUI _text;

    private void Start()
    {
        OnUpdateState += UpdateUI;

        _icon = transform.Find("Icon").GetComponent<Image>();
        _lockedPanel = transform.Find("Lock").GetComponent<Image>();
        _purchaseButton = GetComponent<Button>();
        _text = transform.Find("text").GetComponent<TextMeshProUGUI>();

        _icon.sprite = GeneralData.InfoData.PenguinIcon;
        _purchaseButton.onClick.AddListener(() => legionGeneralSlot.SetSlot(GeneralData.InfoData));
        _purchaseButton.onClick.AddListener(legionGeneralSlot.HidePanel);
    }

    public void UpdateUI()
    {
        if (GeneralData.GeneralDetailData.IsAvailable)
        {
            _purchaseButton.interactable = true;
            _text.gameObject.SetActive(true);
            _lockedPanel.gameObject.SetActive(false);
        } 
        else
        {
            _purchaseButton.interactable = false;
            _text.gameObject.SetActive(false);
            _lockedPanel.gameObject.SetActive(true);
        }      
    }
}
