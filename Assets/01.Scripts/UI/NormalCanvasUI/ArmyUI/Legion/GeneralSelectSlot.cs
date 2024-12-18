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
        OnUpdateState += UpdateUI;

        _icon = transform.Find("Icon").GetComponent<Image>();
        _lockedPanel = transform.Find("Lock").GetComponent<Image>();
        _selectedPanel = transform.Find("Selected").GetComponent<Image>();
        _purchaseButton = GetComponent<Button>();
        _text = transform.Find("text").GetComponent<TextMeshProUGUI>();
        _legionGeneralPanel = transform.parent.GetComponent<LegionGeneralSelectPanel>();

        _icon.sprite = GeneralData.InfoData.PenguinIcon;
        _purchaseButton.onClick.AddListener(() => _legionGeneralPanel.SetSlot(GeneralData.InfoData));
        GeneralData = GeneralManager.Instance.GeneralList[Index];
    }

    private void OnDisable()
    {
        OnUpdateState -= UpdateUI;
    }

    public void SetSelectedPanel()
    {
        _purchaseButton.enabled = false;
        _selectedPanel.gameObject.SetActive(true);
    }

    public void ReverseSelectedPanel()
    {
        _purchaseButton.interactable = true;
        _selectedPanel.gameObject.SetActive(false);
    }

    public void UpdateUI()
    {
        if (GeneralData.GeneralDetailData.IsAvailable)
        {
            if (_text == null)
            {
                Debug.Log(gameObject.name);
            }
            _purchaseButton.interactable = true;
            _text.gameObject.SetActive(true);
            _lockedPanel.gameObject.SetActive(false);
        }
        else
        {
            if (_text == null)
            {
                Debug.Log(gameObject.name);
            }
            _purchaseButton.interactable = false;
            _text.gameObject.SetActive(false);
            _lockedPanel.gameObject.SetActive(true);
        }
    }
}
