using SynergySystem;
using UnityEngine;
using UnityEngine.UI;

public class SoldierSelectSlot : MonoBehaviour
{
    [SerializeField] private SynergyType _synergyType;
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
        _selectButton.onClick.RemoveAllListeners();

        _selectButton.onClick.AddListener(() => parentPanel.currentPanel.SetSlots(_infoSO));
        _selectButton.onClick.AddListener(parentPanel.HidePanel);
        _selectButton.onClick.AddListener(() => SetArmySynergy());
    }

    private void SetArmySynergy()
    {
        //parentPanel.currentPanel.LegionNumber은 1군단이면 1이 담겨잇음
        ArmyManager.Instance.SetArmySynergy(parentPanel.currentPanel.LegionNumber, _synergyType);
    }

    public void OnShowInfo()
    {
        _soliderInfo.SetInfo(_infoSO);
        _soliderInfo.ShowPanel();
    }
}
