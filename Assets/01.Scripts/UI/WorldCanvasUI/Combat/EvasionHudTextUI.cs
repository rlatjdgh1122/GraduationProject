using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasionHudTextUI : WorldUI
{
    public override void Awake()
    {
        base.Awake();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void ShowUI()
    {
        base.ShowUI();

        Sequence seq = DOTween.Sequence();
        seq.Append(canvas.transform.DOMoveY(transform.position.y + 0.8f, 0.35f));
        seq.Insert(0.2f, canvas.DOFade(0, 0.3f));
    }
}
