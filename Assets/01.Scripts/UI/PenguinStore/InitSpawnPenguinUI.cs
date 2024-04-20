using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class InitSpawnPenguinUI : PopupUI
{
    #region Component

    public DummyPenguinFactory PenguinFactory { get; private set; }

    public CanvasGroup StatuCanvas { get; private set; }
    public TextMeshProUGUI StatuesMessageText { get; private set; }

    public BuyPanel BuyPanel { get; private set; }
    protected InfoPanel infoPanel;

    #endregion

    protected DummyPenguin[] penguins;

    [Header("Make Penguin Slot")]
    [SerializeField] private Transform _spawnPenguinButtonParent;
    [SerializeField] private SpawnPenguinButton _spawnPenguinButtonPrefab;
    [SerializeField] protected List<PenguinTypeEnum> _slotLockType;

    protected Dictionary<PenguinTypeEnum, SpawnPenguinButton> lockButtonDicntionary;

    public override void Awake()
    {
        base.Awake();

        PenguinFactory = GameObject.Find("PenguinSpawner/DummyPenguinFactory").GetComponent<DummyPenguinFactory>();
        StatuCanvas = transform.Find("StatusMessage").GetComponent<CanvasGroup>();
        StatuesMessageText = StatuCanvas.transform.Find("WhenBuyPenguin").GetComponent<TextMeshProUGUI>();
        BuyPanel = transform.Find("BuyPanel").GetComponent<BuyPanel>();
        infoPanel = transform.Find("DetailInfoPanel").GetComponent<InfoPanel>();

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