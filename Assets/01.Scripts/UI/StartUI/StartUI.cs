using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartUI : PopupUI
{
    [SerializeField]
    private GameObject BG;

    public override void MovePanel(float x, float y, float fadeTime)
    {
        base.MovePanel(x, y, fadeTime);
    }

    public void OnMovePanel()
    {
        MovePanel(-1900f, 0, 2f);
    }
}
