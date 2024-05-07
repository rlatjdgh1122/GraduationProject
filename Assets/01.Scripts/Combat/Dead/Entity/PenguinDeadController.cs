using UnityEngine;

public class PenguinDeadController : EntityDeadController<Penguin>
{
    //사망할때
    public override void OnDied()
    {
        base.OnDied();

        ArmyManager.Instance.RemovePenguin(_owner.MyArmy.LegionName, _owner);
        var infoData = PenguinManager.Instance.GetInfoDataByPenguin(_owner);
        PenguinManager.Instance.RemoveSoliderPenguin(_owner);
        LegionInventoryManager.Instance.DeadLegionPenguin(infoData.LegionName,infoData.SlotIdx);

        SignalHub.OnModifyCurArmy?.Invoke();
    }

    //부활할때
    public override void OnResurrected()
    {
         base.OnResurrected();

    }
}
