using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupUI : MonoBehaviour
{
    [Header("Popup Setting")] [Space(10f)]
    [SerializeField]
    protected float _panelFadeTime;
    [SerializeField]
    protected float _panelDelayTime;

    [Space(10f)]
    [SerializeField]
    private List<UIType> _transferable;
    public List<UIType> Transferable => _transferable;

    [Space(10f)]
    [SerializeField]
    protected SoundName _soundName = SoundName.None;

    [SerializeField]
    private UIType _uiGroup;
    public UIType UIGroup => _uiGroup;

    protected CanvasGroup _panel;
    protected RectTransform _rectTransform;

    private Coroutine _showCoroutine = null;
    private Coroutine _moveCoroutine = null;
    private Coroutine _showAndHideCoroutine = null;


    public virtual void Awake()
    {
        _panel = GetComponent<CanvasGroup>();
        _rectTransform = GetComponent<RectTransform>();

        if (_panel != null)
        {
            _panel.alpha = 0;
            _panel.blocksRaycasts = false;
        }
    }

    public virtual void ShowPanel()
    {
        if (_showCoroutine != null)
            StopCoroutine(_showCoroutine);

        _showCoroutine = StartCoroutine(ShowPanelCoroutine(_panelDelayTime));
    }

    private IEnumerator ShowPanelCoroutine(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        UIManager.Instance.currentPopupUI.Push(this);
        SoundManager.Play2DSound(_soundName);
        _panel.blocksRaycasts = true;
        _panel.DOFade(1, _panelFadeTime).SetUpdate(true).SetEase(Ease.InSine);
    }

    public virtual void HidePanel()
    {
        SignalHub.OnOffPopUiEvent?.Invoke();

        UIManager.Instance.currentPopupUI.TryPop(out var popupUI);

        SoundManager.Play2DSound(_soundName);

        _panel.blocksRaycasts = false;
        _panel.DOFade(0, _panelFadeTime).SetUpdate(true);
    }

    public virtual void ShowAndHidePanel(float waitTime)
    {
        if (_showAndHideCoroutine != null)
            StopCoroutine(_showAndHideCoroutine);

        _showAndHideCoroutine = StartCoroutine(ShowHideCoroutine(waitTime));
    }

    private IEnumerator ShowHideCoroutine(float waitTime)
    {
        _panel.DOFade(1, _panelFadeTime);
        yield return new WaitForSeconds(waitTime);
        _panel.DOFade(0, _panelFadeTime);
    }

    public virtual void MovePanel(float x, float y, float fadeTime, bool ease = true)
    {
        if (_moveCoroutine != null)
            StopCoroutine(_moveCoroutine);

        _moveCoroutine = StartCoroutine(MovePanelCoroutine(_panelDelayTime, x, y, fadeTime, ease));     
    }

    private IEnumerator MovePanelCoroutine(float waitTime, float x, float y, float fadeTime, bool ease = true)
    {
        yield return new WaitForSeconds(waitTime);
        var tween = _rectTransform.DOAnchorPos(new Vector2(x, y), fadeTime);
        if (ease) tween.SetEase(Ease.OutBack, 0.9f);
    }
}
