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

    public List<ArrangementInfo> InfoList = new();
    private List<Vector3> seatPosList = new();

    public override void Awake()
    {
        SignalHub.OnArrangementInfoModify += SettingArrangement;
    }
    private void OnDestroy()
    {
        SignalHub.OnArrangementInfoModify -= SettingArrangement;
    }

    private void Start()
    {
        Setting();
    }

    private void Setting() // ���� ��ǥ�� ���� ���� ��ǥ�� ���ؼ� �ٶ󺸴� ������� ���ϱ�
    {
        int CLNum = length / 2 + (length % 2 == 0 ? 0 : 1); //7 => 4
        int CWNum = width / 2 + (width % 2 == 0 ? 0 : 1); //5 => 3

        //Debug.Log(CWNum + " : " + CLNum);
        for (int i = (CLNum - 1); i > -CLNum; --i)
        {
            for (int j = -(CWNum - 1); j < CWNum; ++j)
            {
                seatPosList.Add(new Vector3(j * distance, 0, i * distance));
            }
        }
    }

    public void OnClearInfo()
    {
        InfoList.Clear();
    }

    public void OnModifyInfo()
    {
        InfoList.ForEach(p =>
        {
            Penguin obj = null;
            if (p.Type == PenguinTypeEnum.Basic)
            {
                obj = ArmySystem.Instance.CreateSoldier<MeleePenguin>
                (p.Type, SpawnPoint.position, seatPosList[p.SlotIdx]);
            }
            else if (p.Type == PenguinTypeEnum.Shield)
            {
                obj = ArmySystem.Instance.CreateSoldier<ShieldPenguin>
               (p.Type, SpawnPoint.position, seatPosList[p.SlotIdx]);

            }
            else if (p.Type == PenguinTypeEnum.Archer)
            {
                obj = ArmySystem.Instance.CreateSoldier<ArcherPenguin>
               (p.Type, SpawnPoint.position, seatPosList[p.SlotIdx]);
            }
            ArmySystem.Instance.JoinArmyToSoldier(p.legionIdx, obj);
        });
    }

    private void SettingArrangement(ArrangementInfo info)
    {

    }
}