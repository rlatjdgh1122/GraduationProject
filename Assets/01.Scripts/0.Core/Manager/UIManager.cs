using DG.Tweening;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum UIType
{
    Victory,
    Defeat,
    Resource,
}

public class UIManager : Singleton<UIManager>
{
    public Dictionary<UIType, NormalUI> overlayUIDictionary = new Dictionary<UIType, NormalUI>();

    public Vector2 ScreenCenterVec = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
    public Vector2 offVec = new Vector2(Screen.width * 0.5f, -100f);

    public Sequence WarningTextSequence;

    public void InitializeWarningTextSequence()
    {
        WarningTextSequence?.Kill(); // 기존 시퀀스를 중단
        WarningTextSequence = DOTween.Sequence(); // 새로운 시퀀스 할당
    }

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
                Debug.LogWarning("키 중복 : " + overlayUI.name);
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

    public void ChangeTextColorBoolean(TextMeshProUGUI text, bool value, Color beforeColor, Color afterColor, float dureation,
        Ease ease = Ease.Linear, params Action[] actions)
    {
        if (value)
        {
            text.DOColor(beforeColor, dureation).SetEase(ease).OnComplete(() =>
            {
                foreach (Action action in actions)
                {
                    action?.Invoke();
                }
            }); ;
        }
        else
        {
            text.DOColor(afterColor, dureation).SetEase(ease).OnComplete(() =>
            {
                foreach (Action action in actions)
                {
                    action?.Invoke();
                }
            }); ;
        }
    }
    public void ChangeTextColor(TextMeshProUGUI text, Color color, float dureation,
        Ease ease = Ease.Linear, params Action[] actions)
    {
        text.DOColor(color, dureation).SetEase(ease).OnComplete(() =>
        {
            foreach (Action action in actions)
            {
                action?.Invoke();
            }
        }); ;
    }
}
