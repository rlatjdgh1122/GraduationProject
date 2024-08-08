using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LegionSlotPurchaseButton : MonoBehaviour
{
    public PlusSlotPanel _slotPanel;

    [SerializeField] private int idx = 0;
    private LegionSoldierSlot _slot;
    private Button _purchaseButton;
    private LegionPanel _legionPanel;

    private void Awake()
    {
        _slot = GetComponentInChildren<LegionSoldierSlot>();
        _purchaseButton = GetComponent<Button>();
        _legionPanel = transform.parent.GetComponent<LegionPanel>();
    }

    private void Start()
    {
        _slot.gameObject.SetActive(false);
        _purchaseButton.onClick.AddListener(ShowSlotPanel);
    }

    public void ShowSlotPanel()
    {
        _slotPanel.Setting(this);
        _slotPanel.ShowPanel();
    }

    public void Purchase() //»õ½½·Ô Ãß°¡
    {
        CostManager.Instance.SubtractFromCurrentCost(350, () =>
        {
            _purchaseButton.onClick.RemoveListener(ShowSlotPanel);

            _slot.gameObject.SetActive(true);
            _legionPanel.SoldierSlotList.Add(_slot);
            _slot.SetSlot(_legionPanel.SoldierlInfo, _legionPanel.LegionName,_legionPanel.LegionIdx, idx);
        });
    }
}
