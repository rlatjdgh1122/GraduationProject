using DG.Tweening;
using UnityEngine;

public abstract class PopupUI : MonoBehaviour
{
    [SerializeField]
    private float _panelFadeTime;

    protected CanvasGroup _panel;

    public virtual void Awake()
    {
        _panel = GetComponent<CanvasGroup>();
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

    public virtual void ExitButton()
    {
        HidePanel();
    }
}
