using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Windows;

public class PenguinFactory : EntityFactory<Penguin>
{
    protected int spawnZIdx = 0;
    protected int spawnXIdx = 0;

    protected List<Penguin> _curPTspawnPenguins = new List<Penguin>(); // ���� �غ�ð��� ������ ��� ����Ʈ

    public void SpawnPenguinHandler(Penguin penguin)
    {
        if (spawnXIdx >= 5)
        { 
            spawnXIdx = 0;
            spawnZIdx++;
        }

        Vector3 spawnVec = new Vector3(6 + (spawnXIdx * 1.5f),
                                       0.0f,
                                       - 1.5f - (spawnZIdx * 1.5f));

        spawnXIdx++; // ���� ��ġ�� ���� idx

        Penguin spawnPenguin =  SpawnObject(penguin, spawnVec) as Penguin;  //�Ű������� �޾ƿ� Penguin�� �����Ѵ�

        spawnPenguin.SetFreelyMoveAble(true);

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
        WaveManager.Instance.SetCurPTSpawnPenguins(_curPTspawnPenguins);
    }

    protected override PoolableMono Create(Penguin type) // ������ ����� �����Ǵ� �Լ�
    {
        string originalString = type.ToString();
        // �����鿡 �ִ� ��ũ��Ʈ�� �̸��� �������Ƿ� �ʿ��� �κи� �����´�. (ex: MeleePenguin (MeleePenguin) ���� (MeleePenguin)����.)
        string resultString = originalString.Substring(0, originalString.LastIndexOf(" ")); 

        Penguin spawnPenguin = PoolManager.Instance.Pop(resultString) as Penguin;
        spawnPenguin.SetCanInitTent(false);
        return spawnPenguin;
    }
}
