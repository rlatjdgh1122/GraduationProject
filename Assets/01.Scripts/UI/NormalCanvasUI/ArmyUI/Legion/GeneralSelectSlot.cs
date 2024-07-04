using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneralSelectSlot : ArmyComponentUI
{  
    public GeneralStat GeneralData;
    public int Index;
    private Button _purchaseButton;
    private Image _icon;
    private Image _lockedPanel;
    private Image _selectedPanel;
    private TextMeshProUGUI _text;
    private LegionGeneralSelectPanel _legionGeneralPanel;

    private void Start()
    {
        _icon = transform.Find("Icon").GetComponent<Image>();
        _lockedPanel = transform.Find("Lock").GetComponent<Image>();
        _selectedPanel = transform.Find("Selected").GetComponent<Image>();
        _purchaseButton = GetComponent<Button>();
        _text = transform.Find("text").GetComponent<TextMeshProUGUI>();
        _legionGeneralPanel = transform.parent.GetComponent<LegionGeneralSelectPanel>();

        _icon.sprite = GeneralData.InfoData.PenguinIcon;
        _purchaseButton.onClick.AddListener(() => _legionGeneralPanel.SetSlot(GeneralData.InfoData));
        GeneralData = GeneralManager.Instance.GeneralList[Index];

        OnUpdateState += UpdateUI;
    }

    public void SetSelectedPanel()
    {
        OnUpdateState -= UpdateUI;
        _purchaseButton.interactable = false;
        _selectedPanel.gameObject.SetActive(true);
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
