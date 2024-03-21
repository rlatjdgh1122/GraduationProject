using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePanel : PopupUI
{
    [Header("SpawnPenguin")]
    [SerializeField] private Transform UnitInventoryParent;
    [SerializeField] private SpawnPenguinButton _spawnPenguinButtonPrefab;

    public List<PenguinUnitSlot> _slotList;

    public override void Awake()
    {
        base.Awake();

        foreach (var slot in _slotList)
        {
            SpawnPenguinButton btn = Instantiate(_spawnPenguinButtonPrefab, UnitInventoryParent);
            btn.InstantiateSelf(slot.spawnPenguinPrefab.Stat as PenguinStat, slot.spawnPenguinPrefab, slot.price);
            btn.SlotUpdate();
        }
    }

    public override void HidePanel()
    {
        base.HidePanel();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
    }
}
