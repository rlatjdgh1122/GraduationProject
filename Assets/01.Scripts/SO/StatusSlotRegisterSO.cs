using SynergySystem;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatusSlotType
{
    public SynergyType SynergyType;
    public StatusSlot Slot;
}

[CreateAssetMenu(menuName = "SO/Register/StatusSolt")]
public class StatusSlotRegisterSO : ScriptableObject
{
    public List<StatusSlotType> Data = new();
}

