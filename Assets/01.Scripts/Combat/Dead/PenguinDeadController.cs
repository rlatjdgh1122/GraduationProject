using UnityEngine;

public class PenguinDeadController : EntityDeadController<Penguin>, ILiveable
{
    //����Ҷ�
    public override void OnDied()
    {
        base.OnDied();

        ArmyManager.Instance.RemovePenguin(_owner.MyArmy.LegionName, _owner);
        var infoData = PenguinManager.Instance.GetInfoDataByPenguin(_owner);
        PenguinManager.Instance.RemoveSoliderPenguin(_owner);
        LegionInventoryManager.Instance.DeadLegionPenguin(infoData.LegionName,infoData.SlotIdx);

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
