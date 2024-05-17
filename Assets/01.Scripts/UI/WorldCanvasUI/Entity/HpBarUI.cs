using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUI : WorldUI
{
    [SerializeField] private float _waitToDisappear;
    [SerializeField] private Image _hpbar;
    private Coroutine _fadeOutCoroutine;

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
        /*UIManager.Instance.InitializHudTextSequence();

        UIManager.Instance.HudTextSequence.Append(canvas.DOFade(1, 0.5f))
        .Join(_hpbar.DOFillAmount(current / max, 0.5f));*/

        canvas.DOFade(1, 0.5f);
        _hpbar.DOFillAmount(current / max, 0.5f);
    }

    public void FadeOutImmediately()
    {
        canvas?.DOKill();
        canvas.DOFade(0, 0.3f);

        /*UIManager.Instance.InitializHudTextSequence();

        UIManager.Instance.HudTextSequence.Append(canvas.DOFade(0, 0.3f));*/

    }

    private IEnumerator FadeOutTime()
    {
        yield return new WaitForSeconds(_waitToDisappear);

        FadeOutImmediately();
        _fadeOutCoroutine = null;
    }
}