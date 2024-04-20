using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class InitSpawnPenguinUI : PopupUI
{
    #region Component

    public DummyPenguinFactory PenguinFactory { get; private set; }

    public BuyPanel BuyPanel { get; private set; }
    protected InfoPanel infoPanel;
    protected UnLockedPenguinPanel unlockedPenguinPanel;

    #endregion

    protected DummyPenguin[] penguins;

    [Header("Make Penguin Slot")]
    [SerializeField] private Transform _spawnPenguinButtonParent;
    [SerializeField] private SpawnPenguinButton _spawnPenguinButtonPrefab;
    [SerializeField] protected List<PenguinTypeEnum> _slotLockType;

    protected Dictionary<PenguinTypeEnum, SpawnPenguinButton> lockButtonDicntionary = new();
    protected Dictionary<PenguinTypeEnum, PenguinInfoDataSO> penguinInfODictionary = new();

    public override void Awake()
    {
        base.Awake();

        PenguinFactory = GameObject.Find("PenguinSpawner/DummyPenguinFactory").GetComponent<DummyPenguinFactory>();
        BuyPanel = transform.Find("BuyPanel").GetComponent<BuyPanel>();
        infoPanel = transform.Find("DetailInfoPanel").GetComponent<InfoPanel>();
        unlockedPenguinPanel = transform.Find("UnLockedPenguin").GetComponent<UnLockedPenguinPanel>();
        penguins = Resources.LoadAll<DummyPenguin>("PenguinPrefab/Dummy");

        CreateSlot();
    }

    private void CreateSlot()
    {
        foreach (var spawnObj in penguins) //Make Penguin Slot
        {
            if (spawnObj.GetType() == typeof(GeneralDummyPengiun))
            {
                continue;
            }

            var dummyPenguin = spawnObj;
            var UIinfo = spawnObj.NotCloneInfo;


            SpawnPenguinButton btn = Instantiate(_spawnPenguinButtonPrefab, _spawnPenguinButtonParent);

            if (CheckSlotLock(UIinfo.PenguinType))
            {
                penguinInfODictionary.Add(UIinfo.PenguinType, UIinfo);
                lockButtonDicntionary.Add(UIinfo.PenguinType, btn);
            }

            btn.InstantiateSelf(UIinfo, dummyPenguin, UIinfo.Price, CheckSlotLock(UIinfo.PenguinType));
            btn.SlotUpdate();
        }
    }

    protected bool CheckSlotLock(PenguinTypeEnum penguinType)
    {
        foreach (var slotLockType in _slotLockType.Where(slotLockType => penguinType == slotLockType))
        {
            return true;
        }

        return false;
    }
}