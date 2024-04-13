using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralDummyPengiun : DummyPenguin
{
    private void OnMouseDown()
    {
        var infoData = PenguinManager.Instance.GetInfoDataByDummyPenguin<GeneralInfoDataSO>(this);
        var statData = PenguinManager.Instance.GetStatByInfoData<GeneralStat>(infoData);

        PenguinManager.Instance.ShowInfoUI(statData, infoData);
    }
}