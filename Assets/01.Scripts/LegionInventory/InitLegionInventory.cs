using System.Collections.Generic;
using UnityEngine;

public class InitLegionInventory : MonoBehaviour
{
    [SerializeField]
    private int _legionSlotCount;
    [SerializeField]
    private LegionSlot _legionSlot;

    [SerializeField]
    private KeyCode _removeKey = KeyCode.LeftControl;
    [SerializeField]
    private KeyCode _infoKey = KeyCode.Mouse1;

    protected Transform legionInventoryParent;

    protected List<LegionSlot> slotList = new();

    protected LegionInventoryManager legion;

    protected int saveCnt = 0;
    protected int currentPenguinCnt = 0;
    protected int currentRemovePenguinCnt = 0;
    protected int currentGeneral = 0;

    public virtual void Awake()
    {
        legionInventoryParent = transform.Find("LegionInventory/LegionPanel");
        legion = LegionInventoryManager.Instance;

        CreateLegionSlot();
    }

    private void CreateLegionSlot()
    {
        for (int i = 0; i < _legionSlotCount; i++)
        {
            LegionSlot slot = Instantiate(_legionSlot);
            slot.CreateSlot(i, _removeKey, _infoKey);

            slot.transform.parent = legionInventoryParent;
            slotList.Add(slot);
        }
    }

    protected void CheckType(EntityInfoDataSO data)
    {
        if (data.JobType == PenguinJobType.Solider)
            currentPenguinCnt++;
        else
            currentGeneral++;
    }
}
