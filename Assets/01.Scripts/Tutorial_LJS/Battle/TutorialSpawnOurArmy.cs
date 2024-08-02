using ArmySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSpawnOurArmy : MonoBehaviour
{
    [SerializeField] private GeneralInfoDataSO _generalInfoSO;
    [SerializeField] private PenguinInfoDataSO _soliderInfo;

    private void Start()
    {
        ArmyManager.Instance.CreateArmy("1군단");

        SpawnSolider();
        SpawnGeneral();
    }

    private void SpawnGeneral()
    {
        SpawnPenguin(_generalInfoSO);
    }

    private void SpawnSolider()
    {
        for(int i = 0; i < 4; i++)
        {
            SpawnPenguin(_soliderInfo);
        }
    }

    private void SpawnPenguin<T>(T infoData, int index = 0) where T : EntityInfoDataSO
    {
        DummyPenguin dummy = PenguinManager.Instance.SpawnDummyPenguinByInfoData(infoData);
        dummy.CloneInfo.SetLegionName("1군단");

        Penguin penguin = ArmyManager.Instance.SpawnPenguin(dummy.CloneInfo, index);
        PenguinManager.Instance.DummyToPenguinMapping(dummy, penguin);
        ArmyManager.Instance.JoinArmyToSoldier("1군단", 0, penguin);
    }
}
