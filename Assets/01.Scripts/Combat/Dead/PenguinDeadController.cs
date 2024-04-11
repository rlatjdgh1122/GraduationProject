public class PenguinDeadController : EntityDeadController<Penguin>, ILiveable
{
    //사망할때
    public override void OnDied()
    {
        base.OnDied();

        ArmyManager.Instance.RemovePenguin(_owner.MyArmy.LegionName, _owner);
        var legionData = PenguinManager.Instance.GetLegionDataByPenguin(_owner);
        LegionInventoryManager.Instance.DeadLegionPenguin(legionData.Item1, legionData.Item2, legionData.Item3);

        SignalHub.OnModifyCurArmy?.Invoke();
    }

    //부활할때
    public void OnResurrected()
    {
        //죽는 애니메이션 꺼줌
        _anim.SetBool(HASH_DEAD, false);
        //엔티티 네브메쉬와 콜라이더 켜줌
        _agent.enabled = true;
        _character.enabled = true;
        //엔티티 살려줌
        _owner.IsDead = false;
        //엔티티 스크립트 켜줌
        _owner.enabled = true;
    }
}
