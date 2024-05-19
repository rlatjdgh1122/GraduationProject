using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralDummyPengiun : DummyPenguin
{
    public GeneralStat Stat { get; set; } = null;

    private void OnMouseDown()
    {
        if ((UIManager.Instance.currentPopupUI.Count <= 0
            || UIManager.Instance.currentPopupUI.Peek().name == "Masking") && UIManager.Instance.CheckShowAble(UIType.Info))
        {
            PenguinManager.Instance.ShowGeneralInfoUI(this);
            OutlineCompo.enabled = true;
        }
    }
}