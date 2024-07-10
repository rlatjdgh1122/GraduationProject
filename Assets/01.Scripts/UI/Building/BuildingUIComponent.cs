using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUIComponent : MonoBehaviour
{
    [Header("Popup Setting")]
    [Space(10f)]
    [SerializeField]
    protected float panelFadeTime;
    [SerializeField]
    protected float panelDelayTime;

    protected CanvasGroup panel;
    protected RectTransform rectTransform;

    private Coroutine showCoroutine = null;
    private Coroutine _moveCoroutine = null;
    private Coroutine showAndHideCoroutine = null;
    public SynergyBuilding synergyBuilding { get; set; }
    public Health buildingHealth { get; set; }

    public virtual void Awake()
    {
        panel = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();

        if (panel != null)
        {
            panel.alpha = 0;
            panel.blocksRaycasts = false;
        }
    }
    public virtual void ShowPanel()
    {
        if (showCoroutine != null)
            StopCoroutine(showCoroutine);

        showCoroutine = StartCoroutine(ShowPanelCoroutine(panelDelayTime));
    }

    private IEnumerator ShowPanelCoroutine(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        panel.blocksRaycasts = true;
        panel.DOFade(1, panelFadeTime).SetUpdate(true).SetEase(Ease.InSine);
    }

    public virtual void HidePanel()
    {
        panel.blocksRaycasts = false;
        panel.DOFade(0, panelFadeTime).SetUpdate(true);
    }

    public virtual void ShowAndHidePanel(float waitTime)
    {
        if (showAndHideCoroutine != null)
            StopCoroutine(showAndHideCoroutine);

        showAndHideCoroutine = StartCoroutine(ShowHideCoroutine(waitTime));
    }

    private IEnumerator ShowHideCoroutine(float waitTime)
    {
        panel.DOFade(1, panelFadeTime);
        yield return new WaitForSeconds(waitTime);
        panel.DOFade(0, panelFadeTime);
    }

    public virtual void MovePanel(float x, float y, float fadeTime, bool ease = true)
    {
        if (_moveCoroutine != null)
            StopCoroutine(_moveCoroutine);

        _moveCoroutine = StartCoroutine(MovePanelCoroutine(panelDelayTime, x, y, fadeTime, ease));
    }

    private IEnumerator MovePanelCoroutine(float waitTime, float x, float y, float fadeTime, bool ease = true)
    {
        yield return new WaitForSeconds(waitTime);
        var tween = rectTransform.DOAnchorPos(new Vector2(x, y), fadeTime);
        if (ease) tween.SetEase(Ease.OutBack, 0.9f);
    }
}
