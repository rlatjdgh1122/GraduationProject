using ArmySystem;
using SynergySystem;
using System.Collections.Generic;
using UnityEngine;


public class StatusSlotContainer : MonoBehaviour
{
    [SerializeField] private StatusSlotRegisterSO StatusSlotRegisterSO = null;

    private Dictionary<SynergyType, StatusSlot> synergyTypeToSlotDic = new();
    private List<StatusSlot> _statusSlotList = new();

    private void Start()
    {
        foreach (var item in StatusSlotRegisterSO.Data)
        {
            synergyTypeToSlotDic.Add(item.SynergyType, item.Slot);
        }
    }

    /// <summary>
    /// 군단 생성할때 
    /// </summary>
    /// <param name="army"></param>
    public void CreateSlot(Army army)
    {
        var newSlot = GameObject.Instantiate(GetSlotBySynergyType(army.SynergyType), transform);
        newSlot.SetArmy(army);

        _statusSlotList.Add(newSlot);
    }

    /// <summary>
    /// 군단 지워줄때
    /// </summary>
    /// <param name="slot"></param>
    public void RemoveSlot(StatusSlot slot)
    {
        _statusSlotList.Remove(slot);
    }

    private StatusSlot GetSlotBySynergyType(SynergyType type) => synergyTypeToSlotDic[type];

}
