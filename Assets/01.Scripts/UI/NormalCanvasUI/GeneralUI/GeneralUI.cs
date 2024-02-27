using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralUI : NormalUI
{
    public override void Awake()
    {
        base.Awake();
    }

    public override void ExitButtonUI(float time = 0.5F)
    {
        base.ExitButtonUI(time);
    }

    public override void EnableUI(float time, object obj)
    {
        base.EnableUI(time, obj);

        _cvg.DOFade(1, time);
    }

    public override void DisableUI(float time, Action action)
    {
        _cvg.DOFade(0, time);
    }
}
