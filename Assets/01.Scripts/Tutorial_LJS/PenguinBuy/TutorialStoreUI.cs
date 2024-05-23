using System.Collections.Generic;
using UnityEngine;

public class TutorialStoreUI : PopupUI
{
    public TutorialBuyPenguinUI BuyPanel;
    public DummyPenguinFactory PenguinFactory;
    public InfoPanel infoPanel;

    protected DummyPenguin[] penguins;

    [Header("Make Penguin Slot")]
    [SerializeField] private List<TutorialPenguinSpawnButton> _spawnButtonList;

    private List<DummyPenguin> _soliderPenguinList = new();

    public override void Awake()
    {
        base.Awake();

        LoadPenguinSO();
    }

    private void LoadPenguinSO()
    {
        penguins = Resources.LoadAll<DummyPenguin>("PenguinPrefab/Dummy");

        foreach (var spawnObj in penguins) //Make Penguin Slot
        {
            if (spawnObj.GetType() == typeof(GeneralDummyPengiun))
            {
                continue;
            }

            _soliderPenguinList.Add(spawnObj);
        }

        _soliderPenguinList.Sort((a, b) => a.NotCloneInfo.Price.CompareTo(b.NotCloneInfo.Price));

        CreateSlot();
    }

    public void CreateSlot()
    {
        for (int i = 0; i < _spawnButtonList.Count; i++)
        {
            var dummyPenguin = _soliderPenguinList[i];
            var UIinfo = dummyPenguin.NotCloneInfo;

            _spawnButtonList[i].InstantiateSelf(UIinfo, dummyPenguin, UIinfo.Price);
            _spawnButtonList[i].SlotUpdate();
        }
    }

    public void PenguinInformataion(DummyPenguin dummyPenguin, EntityInfoDataSO infoData, int price)
    {
        BuyPanel.PenguinInformataion(dummyPenguin, infoData, price);
        infoPanel.PenguinInformataion(infoData);
    }

    public void ShowLegionPanel()
    {
        UIManager.Instance.HideAllPanel();
        UIManager.Instance.ShowPanel("LegionInventory");
    }
}
