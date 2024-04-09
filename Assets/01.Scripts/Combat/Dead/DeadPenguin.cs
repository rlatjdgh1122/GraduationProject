public class DeadPenguin : DeadEntity<Penguin>, Iliveable
{
    //사망할때
    public override void OnDied()
    {
        base.OnDied();

        //사실 이런 경우가 생기면 안되는데 더미펭귄이 실제 펭귄이라서 같이 싸울때가 있기에 예외처리
        ArmyManager.Instance.Remove(_owner.MyArmy.Legion, _owner);

        var data = PenguinManager.Instance.GetLegionDataByPenguin(_owner);
        LegionInventoryManager.Instance.DeadLegionPenguin(data.Item1, data.Item2, data.Item3);

        SignalHub.OnModifyArmyInfo?.Invoke();
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
