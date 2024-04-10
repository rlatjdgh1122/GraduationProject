public class PenguinDeadController : EntityDeadController<Penguin> , ILiveable
{
    //����Ҷ�
    public override void OnDied()
    {
        base.OnDied();

        //��� �̷� ��찡 ����� �ȵǴµ� ��������� ���� ����̶� ���� �οﶧ�� �ֱ⿡ ����ó��
        ArmyManager.Instance.RemovePenguin(_owner.MyArmy.LegionName, _owner);

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
