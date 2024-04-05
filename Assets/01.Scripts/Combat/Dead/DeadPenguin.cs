public class DeadPenguin : DeadEntity<Penguin>
{
    public override void OnDied()
    {
        base.OnDied();

        //사실 이런 경우가 생기면 안되는데 더미펭귄이 실제 펭귄이라서 같이 싸울때가 있기에 예외처리
        ArmyManager.Instance.Remove(_owner.MyArmy.Legion, _owner);

        SignalHub.OnModifyArmyInfo?.Invoke();
    }
}
