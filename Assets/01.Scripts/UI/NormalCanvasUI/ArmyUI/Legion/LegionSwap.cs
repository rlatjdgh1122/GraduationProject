using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegionSwap : PopupUI
{
    public LegionPanel CurrentPanel;

    [SerializeField] private List<LegionPanel> _legionList;
    private bool _isAnimating = false; 

    public Action<LegionPanel> OnSwapLegionEvent = null;

    public override void Awake()
    {
        base.Awake();
    }

    public override void MovePanel(float x, float y, float fadeTime, bool ease = true)
    {
        base.MovePanel(x, y, fadeTime, ease);
    }

    public void MoveLegionPanel(int swapValue)
    {
        if (_isAnimating) return;

        float moveDistance = 1;
        if (swapValue == 1)
            moveDistance = -1036f;
        else if (swapValue == -1)
            moveDistance = 1036f;

        StartCoroutine(AnimateAndSwap(moveDistance, swapValue));
    }

    private IEnumerator AnimateAndSwap(float moveDistance, int swapValue)
    {
        _isAnimating = true; 
        MovePanel(_rectTransform.anchoredPosition.x + moveDistance, 0, 0.5f, true);
        Swap(swapValue);
        yield return new WaitForSeconds(0.5f); 
        _isAnimating = false; 
    }

    public void Swap(int num)
    {
        foreach (LegionPanel panel in _legionList)
        {
            panel.CurrentIndex += num;

            if (panel.CurrentIndex >= 3)
            {
                panel.MovePanel(panel.PanelTrm.anchoredPosition.x + 5180, -114, 0f);
                panel.CurrentIndex = -2;
            }
            if (panel.CurrentIndex <= -3)
            {
                panel.MovePanel(panel.PanelTrm.anchoredPosition.x - 5180, -114, 0f);
                panel.CurrentIndex = 2;
            }

            if (panel.CurrentIndex == 0)
            {
                panel.PanelTrm.DOScale(new Vector3(2.2f, 2.2f, 1), 0.3f);
                CurrentPanel = panel;
            }
            else
            {
                panel.PanelTrm.DOScale(new Vector3(1.8f, 1.8f, 1), 0.3f);
            }
        }

        if(CurrentPanel != null)
        {
            OnSwapLegionEvent?.Invoke(CurrentPanel);
        }
    }
}
