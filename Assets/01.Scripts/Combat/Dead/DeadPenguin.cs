public class DeadPenguin : DeadEntity<Penguin>, Iliveable
{
    //����Ҷ�
    public override void OnDied()
    {
        base.OnDied();

        //��� �̷� ��찡 ����� �ȵǴµ� ��������� ���� ����̶� ���� �οﶧ�� �ֱ⿡ ����ó��
        ArmyManager.Instance.Remove(_owner.MyArmy.Legion, _owner);

        var data = PenguinManager.Instance.GetLegionDataByPenguin(_owner);
        LegionInventoryManager.Instance.DeadLegionPenguin(data.Item1, data.Item2, data.Item3);

        SignalHub.OnModifyArmyInfo?.Invoke();
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
