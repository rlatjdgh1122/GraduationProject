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

        //_hpbar = transform.GetChild(0).Find("fillAmount").GetComponent<Image>();

        _fadeSequence = DOTween.Sequence();
    }

    public override void Update()
    {
        base.Update();
        //_container.transform.rotation = Quaternion.LookRotation(_container.transform.position - _cam.transform.position);

        if (_hpbar.fillAmount <= 0)
        {
            FadeOutImmediately();
            return;
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
