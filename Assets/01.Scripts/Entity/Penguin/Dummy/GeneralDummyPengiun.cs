using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralDummyPengiun : DummyPenguin
{
    public GeneralStat Stat { get; set; } = null;

    private void OnMouseDown()
    {
        if (UIManager.Instance.CheckShowAble(UIType.Info) && !LegionInventoryManager.Instance.CanUI)
        {
            PenguinManager.Instance.ShowGeneralInfoUI(this);
            OutlineCompo.enabled = true;
        }
    }
}