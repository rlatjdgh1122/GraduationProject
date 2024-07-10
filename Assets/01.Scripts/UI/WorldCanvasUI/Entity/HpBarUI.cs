using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUI : WorldUI
{
    [SerializeField] private float _waitToDisappear;
    [SerializeField] private Image _hpbar;
    private bool CanHideUI => !_health || !_health.IsAlwaysShowUI;

    private Coroutine _coroutine = null;

    private Health _health = null;

    public override void Awake()
    {
        base.Awake();

        _health = transform.root.GetComponent<Health>();
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
        canvas.DOFade(1, 0.5f);
        _hpbar.DOFillAmount(current / max, 0.5f);

        if (!gameObject.activeSelf) return;

        if (CanHideUI) //_health가 없거나
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            _coroutine = StartCoroutine(WaitCorou());
        }
    }


    public override void FadeOutImmediately()
    {
        base.FadeOutImmediately();
    }

    private IEnumerator WaitCorou()
    {
        yield return new WaitForSeconds(1.5f);

        if (!_health || !_health.IsAlwaysShowUI) canvas.DOFade(0, 0.5f);
    }
}