using DG.Tweening;
using UnityEngine;

public class PopupUI : MonoBehaviour
{
    [SerializeField]
    private float _panelFadeTime;

    protected CanvasGroup _panel;
    protected RectTransform _rectTransform;

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
        _panel.blocksRaycasts = true;
        _panel.DOFade(1, _panelFadeTime);
    }

    public virtual void HidePanel()
    {
        _panel.blocksRaycasts = false;
        _panel.DOFade(0, _panelFadeTime);
    }

    public virtual void MovePanel(float x, float y, float fadeTime)
    {
        _rectTransform.DOAnchorPos(new Vector2(x, y), fadeTime).SetEase(Ease.OutBack, 0.9f);
    }
}
