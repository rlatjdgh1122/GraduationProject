using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUI : WorldUI
{
    [SerializeField] private float _waitToDisappear;
    [SerializeField] private Image _hpbar;

    public override void Update()
    {
        base.Update();

        try
        {
            if (_hpbar.fillAmount <= 0)
            {
                FadeOutImmediately();
                return;
            }
        }
        catch
        {
        }
    }

    public void UpdateHpbarUI(float current, float max)
    {
        canvas.DOFade(1, 0.5f);
        _hpbar.DOFillAmount(current / max, 0.5f);
    }

    public override void FadeOutImmediately()
    {
        base.FadeOutImmediately();
    }
}