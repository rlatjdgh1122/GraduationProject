using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
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
    }


    public void OnModifyInfo_Btn() //적용 
    {
        InfoList.ForEach(p =>
        {
            Penguin obj = null;

            /*if (p.Type == PenguinTypeEnum.Basic)
            {
                obj = ArmyManager.Instance.CreateSoldier(p.Type, SpawnPoint.position, seatPosList[p.SlotIdx]);
            }
            else if (p.Type == PenguinTypeEnum.Shield)
            {
                obj = ArmyManager.Instance.CreateSoldier(p.Type, SpawnPoint.position, seatPosList[p.SlotIdx]);

            }
            else if (p.Type == PenguinTypeEnum.Archer)
            {
                obj = ArmyManager.Instance.CreateSoldier(p.Type, SpawnPoint.position, seatPosList[p.SlotIdx]);
            }*/
            obj = ArmyManager.Instance.CreateSoldier(p.Type, SpawnPoint.position, seatPosList[p.SlotIdx]);

            ArmyManager.Instance.JoinArmyToSoldier(p.legionIdx, obj);
        });
    }
}
