using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralDummyPengiun : DummyPenguin
{
    private void OnMouseDown()
    {
        PenguinManager.Instance.ShowInfoUI<GeneralInfoDataSO, GeneralStat>(this);
    }
}