using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingRepairPanel : BuildingUIComponent
{
    public override void Awake()
    {
        base.Awake();
    }

    public void OnMovePanel(float x)
    {
        MovePanel(x, 0, panelFadeTime);
    }
}
