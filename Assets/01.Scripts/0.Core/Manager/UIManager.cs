using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum UIType
{
    Victory,
    Defeat,
}

public class UIManager : Singleton<UIManager>
{
    public Dictionary<UIType, NormalUI> overlayUIDictionary = new Dictionary<UIType, NormalUI>();

    public override void Awake()
    {
        GameObject overlayCanvasRoot = GameObject.Find("Canvas");

        NormalUI[] overlayUIArray = overlayCanvasRoot.GetComponentsInChildren<NormalUI>();

        foreach (NormalUI overlayUI in overlayUIArray)
        {
            if (!overlayUIDictionary.ContainsKey(overlayUI.UIType))
            {
                overlayUIDictionary.Add(overlayUI.UIType, overlayUI);
            }
            else
            {
                Debug.LogWarning("Å° Áßº¹ : " + overlayUI.name);
            }
        }
    }

    public void UIMoveDot(RectTransform transform, Vector3 targetVec, float duration,
                  Ease ease = Ease.Linear, params Action[] actions)
    {
        transform.DOMove(targetVec, duration).SetEase(ease).OnComplete(() =>
        {
            foreach (Action action in actions)
            {
                action?.Invoke();
            }
        });
    }
}
