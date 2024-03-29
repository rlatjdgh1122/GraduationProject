using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUI : WorldUI
{
    [SerializeField] private float _waitToDisappear;
    [SerializeField] private Image _hpbar;
    private Coroutine _fadeOutCoroutine;
    private Sequence _fadeSequence;

    public override void Awake()
    {
        base.Awake();

        _fadeSequence = DOTween.Sequence();
    }

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
        _fadeSequence.Append(canvas.DOFade(1, 0.5f));
        _fadeSequence.Append(_hpbar.DOFillAmount(current / max, 0.5f));

        if (_fadeOutCoroutine != null)
        {
            StopCoroutine(_fadeOutCoroutine);
        }
        _fadeOutCoroutine = StartCoroutine(FadeOutTime());
    }

    public void FadeOutImmediately()
    {
        _fadeSequence.Kill();
        _fadeSequence.Append(canvas.DOFade(0, 0.3f));
    }

    private IEnumerator FadeOutTime()
    {
        yield return new WaitForSeconds(_waitToDisappear);

        FadeOutImmediately();
        _fadeOutCoroutine = null;
    }
}
