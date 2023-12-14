using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public enum UI
{
    Victory,
    Defeat,
}

public class UIManager : Singleton<UIManager>
{
    public Dictionary<UI, PopupUI> uiDictionary = new Dictionary<UI, PopupUI>();

    public override void Awake()
    {
        GameObject root = GameObject.Find("Canvas");

        PopupUI[] popupUIs = root.GetComponentsInChildren<PopupUI>();

        foreach (PopupUI popupUI in popupUIs)
        {
            if (!uiDictionary.ContainsKey(popupUI.UIType))
            {
                uiDictionary.Add(popupUI.UIType, popupUI);
            }
            else
            {
                Debug.LogWarning("Å° Áßº¹ : " + popupUI.name);
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
