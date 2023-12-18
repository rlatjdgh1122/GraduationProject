using UnityEngine;
using UnityEngine.UI;
using Define.CamDefine;
using DG.Tweening;
using System.Collections;
using UnityEngine.EventSystems;

public class HpBarUI : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    private CanvasGroup _container;
    private Image _hpbar;
    private Camera _cam;

    private Coroutine _fadeOutCoroutine;
    private Sequence _fadeSequence;

    private void Awake()
    {
        _cam = Cam.MainCam;
        _hpbar = GetComponent<Image>();
        _container = transform.parent.GetComponent<CanvasGroup>();
        _canvas.worldCamera = _cam;

        _fadeSequence = DOTween.Sequence();
    }

    private void Update()
    {
        _container.transform.rotation = Quaternion.LookRotation(_container.transform.position - _cam.transform.position);
    }

    public void UpdateHpbarUI(float current, float max)
    {
        if (current <= 0)
        {
            FadeOutImmediately();
            return;
        }

        _fadeSequence.Append(_container.DOFade(1, 0.5f));
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
        _fadeSequence.Append(_container.DOFade(0, 0.3f));
    }

    private IEnumerator FadeOutTime()
    {
        yield return new WaitForSeconds(3f);

        FadeOutImmediately();
        _fadeOutCoroutine = null;
    }
}
