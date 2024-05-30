using System.Diagnostics;

public class GeneralInfo
{
    private GeneralStat _generalData;
    private DummyPenguinFactory _penguinFactory;
    private GeneralDummyPengiun _dummyPenguin;

    public GeneralInfo(GeneralStat data, DummyPenguinFactory factory, GeneralDummyPengiun dummy)
    {
        _generalData = data;
        _penguinFactory = factory;
        _dummyPenguin = dummy;
    }

    public void OnPurchase()
    {
        if (CostManager.Instance.Cost >= _generalData.InfoData.Price)
        {
            var spawnDummy = _penguinFactory.SpawnDummyPenguinHandler(_dummyPenguin);
            spawnDummy.Stat = _generalData;
            //LegionInventoryManager.Instance.AddPenguin(_generalData.InfoData);

            CostManager.Instance.Cost -= _generalData.InfoData.Price;
        }
    }
}
