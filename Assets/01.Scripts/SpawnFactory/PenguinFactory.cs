using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Windows;

public class PenguinFactory : EntityFactory<DummyPenguin>
{
    protected int spawnZIdx = 0;
    protected int spawnXIdx = 0;

    protected List<DummyPenguin> _curPTspawnPenguins = new List<DummyPenguin>(); // ���� �غ�ð��� ������ ��� ����Ʈ

    public void SpawnPenguinHandler(DummyPenguin dummyPenguin)
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

        DummyPenguin spawnPenguin = SpawnObject(dummyPenguin, spawnVec) as DummyPenguin;  //�Ű������� �޾ƿ� Penguin�� �����Ѵ�

        _curPTspawnPenguins.Add(spawnPenguin); // ����Ʈ�� �߰�
    }

    private void OnEnable()
    {
        SignalHub.OnIceArrivedEvent += ResetPTInfo;
        SignalHub.OnBattlePhaseStartEvent += SetCurPTspawnPenguins;
    }

    public void OnDisable()
    {
        SignalHub.OnIceArrivedEvent -= ResetPTInfo;
        SignalHub.OnBattlePhaseStartEvent -= SetCurPTspawnPenguins;
    }

    private void ResetPTInfo()
    {
        spawnZIdx = 0;
        spawnXIdx = 0;
        _curPTspawnPenguins.Clear();
    }

    private void SetCurPTspawnPenguins() // �̹� �غ�ð��� ������ ��ϵ��� WaveManager�� �Ѱ��ش�.
    {
        //WaveManager.Instance.SetCurPTSpawnPenguins(_curPTspawnPenguins);
    }

    protected override PoolableMono Create(DummyPenguin _type)
    {
        string originalString = _type.ToString();
        // �����鿡 �ִ� ��ũ��Ʈ�� �̸��� �������Ƿ� �ʿ��� �κи� �����´�. (ex: MeleePenguin (MeleePenguin) ���� (MeleePenguin)����.)
        string resultString = originalString.Substring(0, originalString.LastIndexOf(" "));

        DummyPenguin spawnPenguin = PoolManager.Instance.Pop(resultString) as DummyPenguin;
        //spawnPenguin.SetCanInitTent(false);
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
