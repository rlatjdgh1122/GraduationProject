using System.Collections.Generic;
using UnityEngine;

public class ArrangementManager : Singleton<ArrangementManager>
{
    [SerializeField] private Transform SpawnPoint;

    [Header("배치 거리"), Range(0.5f, 3f)] 
    public float distance = 1;
    private int width = 5;
    private int length = 7;

    private List<ArrangementInfo> CurInfoList = new();
    private List<Vector3> seatPosList = new();

    private MultiKeyDictionary<int, int, Penguin> penguinSpawnDictionary = new();
    public override void Awake()
    {
        Setting();

        SignalHub.OnCompletedGoToHouseEvent += SpawnPenguins;
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
        Debug.Log("이거 외 안되지");
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

    /// <summary>
    /// 군단UI에서 펭귄을 지울때
    /// </summary>
    /// <param name="info"></param>
    private void OnRemoveArmyByInfo(ArrangementInfo info)
    {
        CurInfoList.Remove(info);

        if (penguinSpawnDictionary.TryGetValue(info.Legion, out var value))
        {

            ArmyManager.Instance.Remove(info.Legion, value[info.SlotIdx]);
            penguinSpawnDictionary[info.Legion].Remove(info.SlotIdx);
        }
    }
    private void OnJoinArmyByInfo(ArrangementInfo info)
    {
        Penguin obj = PenguinManager.Instance.SpawnSoldier(info.PenguinType, SpawnPoint.position, seatPosList[info.SlotIdx]);

        if (info.JobType == PenguinJobType.Solider)
        {
            ArmyManager.Instance.JoinArmyToSoldier(info.Legion, obj);
        }

        if (info.JobType == PenguinJobType.General)
        {
            ArmyManager.Instance.JoinArmyToGeneral(info.Legion, obj as General);
        }

        PenguinManager.Instance.SetOwnerDummyPenguin(info.PenguinType, obj);
        penguinSpawnDictionary.Add(info.Legion, info.SlotIdx, obj);
    }
    private void OnDestroy()
    {
        SignalHub.OnCompletedGoToHouseEvent -= SpawnPenguins;
    }
}
