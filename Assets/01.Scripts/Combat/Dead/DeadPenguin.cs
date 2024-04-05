public class DeadPenguin : DeadEntity<Penguin>
{
    public override void OnDied()
    {
        base.OnDied();

        //��� �̷� ��찡 ����� �ȵǴµ� ��������� ���� ����̶� ���� �οﶧ�� �ֱ⿡ ����ó��
        ArmyManager.Instance.Remove(_owner.MyArmy.Legion, _owner);

        SignalHub.OnModifyArmyInfo?.Invoke();
    }
}
