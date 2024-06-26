using UnityEngine;
using UnityEngine.UI;

public class SoldierSelectSlot : MonoBehaviour
{
    [SerializeField] private EntityInfoDataSO _infoSO;

    private SoldierInfo _soliderInfo;
    private Button _selectButton;

    public SoldierSelectPanel parentPanel;

    private void Awake()
    {
        _soliderInfo = transform.Find("Info").GetComponent<SoldierInfo>();
        _selectButton = GetComponent<Button>();
    }

    public void SetButtonListener()
    {
        _selectButton.onClick.AddListener(() => parentPanel.currentPanel.SetSlots(_infoSO));
        _selectButton.onClick.AddListener(() => CostManager.Instance.SubtractFromCurrentCost(500));
        _selectButton.onClick.AddListener(parentPanel.HidePanel);
    }

    public void OnShowInfo()
    {
        _soliderInfo.SetInfo(_infoSO);
        _soliderInfo.ShowPanel();
    }
}
