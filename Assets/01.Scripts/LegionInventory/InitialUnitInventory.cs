using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialUnitInventory : MonoBehaviour
{
    protected Dictionary<EntityInfoDataSO, UnitSlotUI> penguinDictionary = new();

    [SerializeField]
    private UnitSlotUI _slotPrefab;

    protected Transform spawnPenguinSlotParent;
    protected Transform spawnGeneralSlotParent;

    protected virtual void Awake()
    {
        spawnPenguinSlotParent = transform.Find("PenguinUnitSlots");
        spawnGeneralSlotParent = transform.Find("GeneralUnitSlots");

        LoadPenguinSO();
    }

    /// Penguin 폴더 안에 있는 모든 EntityInfoDataSO 불러오기
    private void LoadPenguinSO()
    {
        EntityInfoDataSO[] infoDatas = Resources.LoadAll<EntityInfoDataSO>("Penguin");

        foreach (var so in infoDatas)
        {
            CreateUnitSlot(so);
        }
    }

    /// 개수만큼 유닛 슬롯 추가
    private void CreateUnitSlot(EntityInfoDataSO so)
    {
        UnitSlotUI slot = Instantiate(_slotPrefab);
        slot.Create(so);
        
        if (so is GeneralInfoDataSO)
        {
            slot.transform.parent = spawnGeneralSlotParent;
        }
        else
        {
            slot.transform.parent = spawnPenguinSlotParent;
        }

        penguinDictionary.Add(so, slot);
    }
}
