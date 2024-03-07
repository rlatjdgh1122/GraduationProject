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

    private List<Penguin> _spawnPenguins = new(); //���� ���� �� �ż� �̹� ���� ����� ���� �� ��ϵ�

    private void Start()
    {
        Setting();

        WaveManager.Instance.OnDummyPenguinInitTentFinEvent += SpawnPenguins;
    }

    private void Setting() // ���� ��ǥ�� ���� ���� ��ǥ�� ���ؼ� �ٶ󺸴� ������� ���ϱ�
    {
        int CLNum = length / 2 + (length % 2 == 0 ? 0 : 1); //7 => 4
        int CWNum = width / 2 + (width % 2 == 0 ? 0 : 1); //5 => 3

        //Debug.Log(CWNum + " : " + CLNum);
        for (int i = (CLNum - 1); i > -CLNum; --i) //���� 
        {
            for (int j = -(CWNum - 1); j < CWNum; ++j) //����
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

        OnModifyInfo_Btn(info);
    }

    private void SpawnPenguins()
    {
        for(int i = 0; i < _spawnPenguins.Count; i++)
        {
            _spawnPenguins[i].gameObject.SetActive(true);
        }
        _spawnPenguins.Clear();
    }

    private void OnModifyInfo_Btn(ArrangementInfo info) //���� 
    {
        if(info.JobType == PenguinJobType.Solider)
        {
            Penguin obj = null;
            obj = ArmyManager.Instance.CreateSoldier(info.PenguinType, SpawnPoint.position, seatPosList[info.SlotIdx]);
            ArmyManager.Instance.JoinArmyToSoldier(info.legion, obj as Penguin);
            _spawnPenguins.Add(obj);
        }

        if (info.JobType == PenguinJobType.General)
        {
              General obj = null;
              obj = ArmyManager.Instance.CreateSoldier(info.PenguinType, SpawnPoint.position, seatPosList[info.SlotIdx]) as General;

              ArmyManager.Instance.JoinArmyToGeneral(info.legion, obj);
            _spawnPenguins.Add(obj);
        }
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
