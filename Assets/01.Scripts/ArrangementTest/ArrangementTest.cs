using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.Jobs.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class ArrangementTest : Singleton<ArrangementTest>
{
    [SerializeField] private Transform SpawnPoint;

    [SerializeField] private float distance = 3;
    [SerializeField] private int width = 5;
    [SerializeField] private int length = 7;

    [SerializeField] private List<ArrangementInfo> InfoList = new();
    private List<Vector3> seatPosList = new();

    private Queue<Action> _spawnPenguinArmyQueue = new(); // 이번 배틀 페이즈에 생성될 군단에 포함된 펭귄들

    private void Start()
    {
        Setting();
    }

    private void Setting() // 이전 좌표랑 새로 찍은 좌표랑 비교해서 바라보는 방향까지 정하기
    {
        int CLNum = length / 2 + (length % 2 == 0 ? 0 : 1); //7 => 4
        int CWNum = width / 2 + (width % 2 == 0 ? 0 : 1); //5 => 3

        //Debug.Log(CWNum + " : " + CLNum);
        for (int i = (CLNum - 1); i > -CLNum; --i) //세로 
        {
            for (int j = -(CWNum - 1); j < CWNum; ++j) //가로
            {
                seatPosList.Add(new Vector3(j * distance, 0, i * distance));
            }
        }
    }

    public void OnClearArrangementInfo()
    {
        InfoList.Clear();
    }
    public void AddArrangementInfo(ArrangementInfo info)
    {
        InfoList.Add(info);

        Action subscribeAction = () => OnModifyInfo_Btn(info);
        _spawnPenguinArmyQueue.Enqueue(subscribeAction);

        // 이벤트에 메서드를 구독
        WaveManager.Instance.OnDummyPenguinInitTentFinEvent += subscribeAction;

        Debug.Log("구독합니다");
    }


    private void OnModifyInfo_Btn(ArrangementInfo info) //적용 
    {
        if(info.JobType == PenguinJobType.Solider)
        {
            Penguin obj = null;
            obj = ArmyManager.Instance.CreateSoldier(info.PenguinType, SpawnPoint.position, seatPosList[info.SlotIdx]);
            ArmyManager.Instance.JoinArmyToSoldier(info.legionIdx, obj as Penguin);
        }

        if (info.JobType == PenguinJobType.General)
        {
              General obj = null;
              obj = ArmyManager.Instance.CreateSoldier(info.PenguinType, SpawnPoint.position, seatPosList[info.SlotIdx]) as General;

              ArmyManager.Instance.JoinArmyToGeneral(info.legionIdx, obj);
        }

        WaveManager.Instance.OnDummyPenguinInitTentFinEvent -= _spawnPenguinArmyQueue.Dequeue();

        //InfoList.ForEach(p =>
        //{

        //    if (p.JobType == PenguinJobType.Solider)
        //    {
        //        Penguin obj = null;
        //        obj = ArmyManager.Instance.CreateSoldier(p.PenguinType, SpawnPoint.position, seatPosList[p.SlotIdx]);

        //        ArmyManager.Instance.JoinArmyToSoldier(p.legionIdx, obj as Penguin);
        //    }

        //    if (p.JobType == PenguinJobType.General)
        //    {
        //        General obj = null;
        //        obj = ArmyManager.Instance.CreateSoldier(p.PenguinType, SpawnPoint.position, seatPosList[p.SlotIdx]) as General;

        //        ArmyManager.Instance.JoinArmyToGeneral(p.legionIdx, obj);
        //    }
        //});
    }
}
