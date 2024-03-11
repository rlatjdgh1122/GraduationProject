using System;
using System.Collections.Generic;
using UnityEngine;

public class ArrangementManager : Singleton<ArrangementManager>
{
    [SerializeField] private Transform SpawnPoint;

    private float distance = 1;
    private int width = 5;
    private int length = 7;

    private List<ArrangementInfo> CurInfoList = new();
    private List<Vector3> seatPosList = new();

    private MultiKeyDictionary<int, int, Penguin> penguinSpawnDictionary = new();

    private int prevUILegion = 0;
    private int curUILegion = 0;
    private void Start()
    {
        Setting();
        SignalHub.OnUILegionChanged += OnUILegionHandler;
        WaveManager.Instance.OnDummyPenguinInitTentFinEvent += SpawnPenguins;
    }

    private void OnUILegionHandler(int prevLegion, int newLegion)
    {
        prevUILegion = prevLegion;
        curUILegion = newLegion;
    }

    private void Setting()
    {
        int CLNum = length / 2 + (length % 2 == 0 ? 0 : 1); //7 => 4
        int CWNum = width / 2 + (width % 2 == 0 ? 0 : 1); //5 => 3

        for (int i = (CLNum - 1); i > -CLNum; --i)
        {
            for (int j = -(CWNum - 1); j < CWNum; ++j)
            {
                seatPosList.Add(new Vector3(j * distance, 0, i * distance));
            }
        }
    }

    public void OnClearArrangementInfo()
    {
        CurInfoList.Clear();
    }

    public void AddArrangementInfo(ArrangementInfo info)
    {
        CurInfoList.Add(info);

        OnJoinArmyByInfo(info);
    }
    public void RemoveArrangementInfoByLegionAndSlotIdx(int legion, int slotIdx)
    {
        var findInfo =
            CurInfoList.Find(x => x.Legion == legion && x.SlotIdx == slotIdx);

        OnRemoveArmyByInfo(findInfo);
    }

    private void SpawnPenguins()
    {
        foreach (var p in penguinSpawnDictionary)
        {
            foreach (var q in p.Value)
            {
                var key = q.Key;
                var value = p.Value[key];

                value.gameObject.SetActive(true);
            }
        }

        penguinSpawnDictionary.Clear();
    }

    private void OnRemoveArmyByInfo(ArrangementInfo info)
    {
        CurInfoList.Remove(info);

        if (penguinSpawnDictionary.TryGetValue(info.Legion, out var value))
        {
            Debug.Log($"[����] {info.Legion}���� {info.SlotIdx}��");

            ArmyManager.Instance.Remove(info.Legion, value[info.SlotIdx]);
            penguinSpawnDictionary[info.Legion].Remove(info.SlotIdx);
        }
    }
    private void OnJoinArmyByInfo(ArrangementInfo info)
    {
        if (info.JobType == PenguinJobType.Solider)
        {
            Penguin obj = null;
            obj = ArmyManager.Instance.CreateSoldier(info.PenguinType, SpawnPoint.position, seatPosList[info.SlotIdx]);

            Debug.Log($"[�߰�] {info.Legion}���� {info.SlotIdx}�� ({obj})");

            ArmyManager.Instance.JoinArmyToSoldier(info.Legion, obj as Penguin);
            penguinSpawnDictionary.Add(info.Legion, info.SlotIdx, obj);
        }

        if (info.JobType == PenguinJobType.General)
        {
            General obj = null;
            obj = ArmyManager.Instance.CreateSoldier(info.PenguinType, SpawnPoint.position, seatPosList[info.SlotIdx]) as General;

            ArmyManager.Instance.JoinArmyToGeneral(info.Legion, obj);
            penguinSpawnDictionary.Add(info.Legion, info.SlotIdx, obj);
        }
    }

    public void OnModify_Btn()
    {

    }
}
