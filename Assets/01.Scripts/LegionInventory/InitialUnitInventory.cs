using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InitialUnitInventory : MonoBehaviour, ICreateSlotUI
{
    protected Dictionary<EntityInfoDataSO, UnitSlotUI> penguinDictionary = new();
    protected Dictionary<PenguinTypeEnum, UnitSlotUI> lockButtonDicntionary = new();

    public List<DummyPenguin> _infoDataSOList = new();

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
        DummyPenguin[] infoDatas = Resources.LoadAll<DummyPenguin>("PenguinPrefab/Dummy");

        foreach (var so in infoDatas)
        {
            _infoDataSOList.Add(so);
        }

        _infoDataSOList.Sort((a, b) => a.NotCloneInfo.Price.CompareTo(b.NotCloneInfo.Price));

        CreateSlot();
    }

    /// 개수만큼 유닛 슬롯 추가
    public void CreateSlot()
    {
        foreach (var so in _infoDataSOList)
        {
            UnitSlotUI slot = Instantiate(_slotPrefab);

            var UIinfo = so.NotCloneInfo;

            lockButtonDicntionary.Add(UIinfo.PenguinType, slot);

            slot.Create(UIinfo);

            if (UIinfo is GeneralInfoDataSO)
            {
                slot.transform.SetParent(spawnGeneralSlotParent);
            }
            else
            {
                slot.transform.SetParent(spawnPenguinSlotParent);
            }

            penguinDictionary.Add(UIinfo, slot);
        }
    }
}
