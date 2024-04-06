using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitLegionInventory : MonoBehaviour
{
    [SerializeField]
    private int _legionSlotCount;
    [SerializeField]
    private LegionSlotUI _legionSlot;

    [SerializeField]
    private KeyCode _removeKey = KeyCode.LeftControl;
    [SerializeField]
    private KeyCode _infoKey = KeyCode.Mouse1;

    protected Transform _legionInventoryParent;

    public virtual void Awake()
    {
        _legionInventoryParent = transform.Find("LegionInventory/LegionPanel").GetComponent<Transform>();

        CreateLegionSlot();
    }

    private void CreateLegionSlot()
    {
        for (int i = 0; i < _legionSlotCount; i++)
        {
            LegionSlotUI slot = Instantiate(_legionSlot);
            slot.transform.parent = _legionInventoryParent;
            slot.CreateSlot(i, _removeKey, _infoKey);
        }
    }
}
