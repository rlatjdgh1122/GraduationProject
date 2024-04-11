public class PenguinDeadController : EntityDeadController<Penguin>, ILiveable
{
    //����Ҷ�
    public override void OnDied()
    {
        base.OnDied();

        ArmyManager.Instance.RemovePenguin(_owner.MyArmy.LegionName, _owner);
        var legionData = PenguinManager.Instance.GetLegionDataByPenguin(_owner);
        LegionInventoryManager.Instance.DeadLegionPenguin(legionData.Item1, legionData.Item2, legionData.Item3);

        SignalHub.OnModifyCurArmy?.Invoke();
    }

    //��Ȱ�Ҷ�
    public void OnResurrected()
    {
        //�״� �ִϸ��̼� ����
        _anim.SetBool(HASH_DEAD, false);
        //��ƼƼ �׺�޽��� �ݶ��̴� ����
        _agent.enabled = true;
        _character.enabled = true;
        //��ƼƼ �����
        _owner.IsDead = false;
        //��ƼƼ ��ũ��Ʈ ����
        _owner.enabled = true;
    }
}
