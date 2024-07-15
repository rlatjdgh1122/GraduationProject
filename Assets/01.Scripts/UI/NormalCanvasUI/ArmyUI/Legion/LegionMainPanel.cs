using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegionMainPanel : PopupUI
{
    public override void Awake()
    {
        base.Awake();
    }

    public void MoveMainPanel(float x)
    {
        MovePanel(x, 0, _panelFadeTime, true);
    }

    public override void MovePanel(float x, float y, float fadeTime, bool ease = true)
    {
        base.MovePanel(x, y, fadeTime, ease);

        SoundManager.Play2DSound(_soundName);
    }
}
