using DG.Tweening;
using UnityEngine;

public class PopupUI : MonoBehaviour
{
    [SerializeField]
    private float _panelFadeTime;

    protected CanvasGroup _panel;

    public virtual void Awake()
    {
        _panel = GetComponent<CanvasGroup>();

        if (_panel != null)
        {
            _panel.alpha = 0;
            _panel.blocksRaycasts = false;
        }
    }

    public virtual void ShowPanel()
    {
        _panel.blocksRaycasts = true;
        _panel.DOFade(1, _panelFadeTime);
    }

    public virtual void HidePanel()
    {
        _panel.blocksRaycasts = false;
        _panel.DOFade(0, _panelFadeTime);
    }
}
