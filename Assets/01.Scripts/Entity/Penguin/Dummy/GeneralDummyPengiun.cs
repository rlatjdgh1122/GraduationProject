using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralDummyPengiun : DummyPenguin
{
    public AnimationClip[] a;
    private void OnMouseDown()
    {
        var infoData = PenguinManager.Instance.GetInfoDataByDummyPenguin<GeneralInfoDataSO>(this);
    }
}
