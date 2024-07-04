using UnityEngine;

public class PenguinDeadController : EntityDeadController<Penguin>
{
    //사망할때

    private Outline OutlineCompo = null;

    protected override void Awake()
    {
        base.Awake();

        OutlineCompo = GetComponent<Outline>();
    }
    public override void OnDied()
    {
        base.OnDied();

        OutlineCompo.enabled = false;

        ArmyManager.Instance.RemovePenguin(_owner.MyArmy.LegionName, _owner);
        var infoData = PenguinManager.Instance.GetInfoDataByPenguin(_owner);
        PenguinManager.Instance.DeadSoliderPenguin(_owner);
        //LegionInventoryManager.Instance.DeadLegionPenguin(infoData.LegionName, infoData.SlotIdx);

        SignalHub.OnOurPenguinDead?.Invoke();
        SignalHub.OnModifyCurArmy?.Invoke();
    }

    //부활할때
    public override void OnResurrected()
    {
        base.OnResurrected();

    }
}
