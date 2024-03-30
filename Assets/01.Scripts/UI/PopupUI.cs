using DG.Tweening;
using System.Collections;
using UnityEngine;

public class PopupUI : MonoBehaviour
{
    [SerializeField]
    private float _panelFadeTime;

    [SerializeField]
    private SoundName soundName = SoundName.UI;

    protected CanvasGroup _panel;
    protected RectTransform _rectTransform;

    private Coroutine _coroutine = null;


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
        UIManager.Instance.currentPopupUI.Push(this);
        SoundManager.Play2DSound(soundName);
        _panel.blocksRaycasts = true;
        _panel.DOFade(1, _panelFadeTime);
    }

    public virtual void HidePanel()
    {
        SignalHub.OnOffPopUiEvent?.Invoke();

        UIManager.Instance.currentPopupUI.TryPop(out var popupUI);
        SoundManager.Play2DSound(soundName);

        _panel.blocksRaycasts = false;
        _panel.DOFade(0, _panelFadeTime);
    }

    public virtual void ShowAndHidePanel(float waitTime)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(ShowHideCoroutine(waitTime));
    }

    private IEnumerator ShowHideCoroutine(float waitTime)
    {
        _panel.DOFade(1, _panelFadeTime);
        yield return new WaitForSeconds(waitTime);
        _panel.DOFade(0, _panelFadeTime);
    }

    public virtual void MovePanel(float x, float y, float fadeTime)
    {
        _rectTransform.DOAnchorPos(new Vector2(x, y), fadeTime).SetEase(Ease.OutBack, 0.9f);
    }
}
