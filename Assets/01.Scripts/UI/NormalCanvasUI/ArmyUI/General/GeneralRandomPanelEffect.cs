using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralRandomPanelEffect : MonoBehaviour
{
    private CanvasGroup _canvasgroup;
    private Coroutine _showCoroutine;

    [SerializeField] private float _fadeTime;
    [SerializeField] private float _delayTime;

    private void Awake()
    {
        _canvasgroup = GetComponent<CanvasGroup>();
    }

    public void ShowPanel()
    {
        if (_showCoroutine != null)
            StopCoroutine( _showCoroutine );

        _showCoroutine = StartCoroutine(ShowPanelCoroutine(_delayTime));
    }

    private IEnumerator ShowPanelCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        _canvasgroup.DOFade(1, _fadeTime).SetEase(Ease.InSine).OnComplete(() => HidePanel());
    }

    public void HidePanel()
    {
        _canvasgroup.DOFade(0, 0.3f);
    }
}
