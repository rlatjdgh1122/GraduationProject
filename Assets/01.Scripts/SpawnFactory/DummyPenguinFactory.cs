using System.Collections.Generic;
using UnityEngine;

public class DummyPenguinFactory : EntityFactory<DummyPenguin>
{
    protected int spawnZIdx = 0;
    protected int spawnXIdx = 0;

    protected List<DummyPenguin> _dummyPenguins = new List<DummyPenguin>(); // ���� �غ�ð��� ������ ��� ����Ʈ


    private void OnEnable()
    {
        SignalHub.OnIceArrivedEvent += ResetPTInfo;
        SignalHub.OnBattlePhaseStartEvent += SetupComplete;
    }

    public void OnDisable()
    {
        SignalHub.OnIceArrivedEvent -= ResetPTInfo;
        SignalHub.OnBattlePhaseStartEvent -= SetupComplete;
    }

    /// <summary>
    /// ���� ��� ����
    /// </summary>
    /// <param name="dummyPenguin"> ���� ���</param>   
    public void SpawnDummyPenguinHandler(DummyPenguin dummyPenguin)
    {
        if (spawnXIdx >= 5)
        {
            spawnXIdx = 0;
            spawnZIdx++;
        }

        Vector3 spawnVec = new Vector3(6 + (spawnXIdx * 1.5f),
                                       0.0f,
                                       -1.5f - (spawnZIdx * 1.5f));

        spawnXIdx++; // ���� ��ġ�� ���� idx

        DummyPenguin spawnPenguin = SpawnObject(dummyPenguin, spawnVec) as DummyPenguin;

        _dummyPenguins.Add(spawnPenguin); // ����Ʈ�� �߰�
    }

    /// <summary>
    /// ������ ���� ����� ��Ʋ ���尡 �Ǹ� �����Ŵ����� ���
    /// </summary>
    private void SetupComplete()
    {
        SpawnManager.Instance.GetDummyPenguinList(_dummyPenguins);
    }

    private void ResetPTInfo()
    {
        spawnZIdx = 0;
        spawnXIdx = 0;
        _dummyPenguins.Clear();
    }
    protected override PoolableMono Create(DummyPenguin _type)
    {
        string originalString = _type.ToString();
        // �����鿡 �ִ� ��ũ��Ʈ�� �̸��� �������Ƿ� �ʿ��� �κи� �����´�. (ex: MeleePenguin (MeleePenguin) ���� (MeleePenguin)����.)
        string resultString = originalString.Substring(0, originalString.LastIndexOf(" "));

        DummyPenguin spawnPenguin = PoolManager.Instance.Pop(resultString) as DummyPenguin;
        return spawnPenguin;
    }

    /*  protected override PoolableMono Create(Penguin type) // ������ ����� �����Ǵ� �Լ�
      {
          string originalString = type.ToString();
          // �����鿡 �ִ� ��ũ��Ʈ�� �̸��� �������Ƿ� �ʿ��� �κи� �����´�. (ex: MeleePenguin (MeleePenguin) ���� (MeleePenguin)����.)
          string resultString = originalString.Substring(0, originalString.LastIndexOf(" "));

          Penguin spawnPenguin = PoolManager.Instance.Pop(resultString) as Penguin;
          spawnPenguin.SetCanInitTent(false);
          return spawnPenguin;
      }*/
}
