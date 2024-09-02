using ArmySystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpawnOurArmy : MonoBehaviour
{
    [SerializeField] private GeneralInfoDataSO _generalInfoSO;
    [SerializeField] private PenguinInfoDataSO _soliderInfo;

    [SerializeField] private TutorialWorldCanvas _arrow;
    [SerializeField] private TutorialController _controller;

    private string _legionName = "1";

    private void Start()
    {
        ArmyManager.Instance.CreateArmy();

        SpawnSolider();
        SpawnGeneral();
    }

    private void SpawnGeneral()
    {
        var dummy = SpawnDuumyPenguin(_generalInfoSO);
        GeneralJoinArmy(dummy, 0);

        _arrow.Init(TutorialImage.Warning);
        _arrow.SetTarget(dummy.transform, 4.5f);
    }

    public void CllickDummyPenguin()
    {
        _controller.TutorialInfoUI.CompleteSlot(_controller.CurrentTutorial(0));
        TutorialM.Instance.AddTutorialIndex();
    }

    private void SpawnSolider()
    {
        ArmyManager.Instance.GetArmyByLegionName(_legionName).SynergyType = SynergySystem.SynergyType.IceCream;

        for (int i = 1; i <= 4; i++)
        {
            SoliderJoinArmy(SpawnDuumyPenguin(_soliderInfo), i);
        }
    }

    private DummyPenguin SpawnDuumyPenguin<T>(T infoData) where T : EntityInfoDataSO
    {
        DummyPenguin dummy = PenguinManager.Instance.SpawnDummyPenguinByInfoData(infoData);
        dummy.CloneInfo.SetLegionName(_legionName);

        return dummy;
    }

    private void SoliderJoinArmy(DummyPenguin dummy, int index)
    {
        ArmyManager.Instance.JoinArmyToSoldier(_legionName, 0, SpawnPenguin(dummy, index));
    }

    private void GeneralJoinArmy(DummyPenguin dummy, int index)
    {
        General general = SpawnPenguin(dummy, index) as General;
        ArmyManager.Instance.JoinArmyToGeneral(_legionName, 0, general);
    }

    private Penguin SpawnPenguin(DummyPenguin dummy, int index)
    {
        Penguin penguin = ArmyManager.Instance.SpawnPenguin(dummy.CloneInfo, index);
        PenguinManager.Instance.DummyToPenguinMapping(dummy, penguin);

        return penguin;
    }
}
