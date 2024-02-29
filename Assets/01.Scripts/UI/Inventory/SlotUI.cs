using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class SlotUI : MonoBehaviour
{
    [SerializeField] protected Image _unitImage;
    [SerializeField] protected Sprite _emptyImage;
    public LegionInventoryData _data = null;
    protected LegionInventoryUI _legionInventoryUI;

    protected virtual void Awake()
    {
        _legionInventoryUI = transform.parent.parent.parent.parent.parent.GetComponent<LegionInventoryUI>();
    }

    public abstract void UpdateSlot(LegionInventoryData data);
    public abstract void CleanUpSlot();
}
