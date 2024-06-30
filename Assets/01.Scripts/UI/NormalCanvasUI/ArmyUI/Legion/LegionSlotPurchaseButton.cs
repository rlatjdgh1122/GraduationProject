using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LegionSlotPurchaseButton : MonoBehaviour
{
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
        _purchaseButton.onClick.AddListener(Purchase);
    }

    private void Purchase()
    {
        CostManager.Instance.SubtractFromCurrentCost(350, () =>
        {
            _slot.gameObject.SetActive(true);
            _legionPanel.SoldierSlotList.Add(_slot);
            _slot.SetSlot(_legionPanel.SoldierlInfo);
        });
    }
}
