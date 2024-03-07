using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum UIType
{
    Victory,
    Defeat,
    Resource,
    Nexus,
    General
}

public class UIManager : Singleton<UIManager>
{
    public Dictionary<UIType, NormalUI> overlayUIDictionary = new Dictionary<UIType, NormalUI>();
    public Dictionary<string, PopupUI> popupUIDictionary = new Dictionary<string, PopupUI>();

    public Transform canvasTrm;

    public Vector2 ScreenCenterVec = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
    public Vector2 offVec = new Vector2(Screen.width * 0.5f, -100f);

    public Sequence HudTextSequence;

    public void InitializHudTextSequence()
    {
        HudTextSequence?.Kill(); // 기존 시퀀스를 중단
        HudTextSequence = DOTween.Sequence(); // 새로운 시퀀스 할당
    }

    public override void Awake()
    {
        canvasTrm = GameObject.Find("Canvas").transform;

        NormalUI[] overlayUIArray = canvasTrm.GetComponentsInChildren<NormalUI>();
        PopupUI[] popupUIs = canvasTrm.GetComponentsInChildren<PopupUI>();

        foreach (PopupUI popupUI in popupUIs)
        {
            if (!popupUIDictionary.ContainsKey(popupUI.name))
            {
                popupUIDictionary.Add(popupUI.name, popupUI);
            }
            else
            {
                Debug.Log($"중복 키 : {popupUI.name}");
            }
        }

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

        foreach (PopupUI popup in popupUIDictionary.Values)
        {
            Debug.Log(popup.name);
        }
    }

    public void ShowPanel(string uiName)
    {
        popupUIDictionary.TryGetValue(uiName, out PopupUI popupUI);
        if (popupUI != null)
            popupUI.ShowPanel();
    }

    public void HidePanel(string uiName)
    {
        popupUIDictionary.TryGetValue(uiName, out PopupUI popupUI);
        popupUI.HidePanel();
    }

    public IEnumerator UIMoveDotCoroutine(RectTransform transform, Vector3 targetVec, float duration,
                                      Ease ease = Ease.Linear, params Action[] actions)
    {
        yield return transform.DOMove(targetVec, duration).SetEase(ease).WaitForCompletion();

        foreach (Action action in actions)
        {
            action?.Invoke();
        }
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

    public void SpawnHudText(TextMeshProUGUI text)
    {
        InitializHudTextSequence();
        HudTextSequence.Prepend(text.DOFade(1f, 0.5f))
        .Join(text.rectTransform.DOMoveY(ScreenCenterVec.y, 0.5f))
        .Append(text.DOFade(0f, 0.5f))
        .Join(text.rectTransform.DOMoveY(ScreenCenterVec.y - 50f, 0.5f));
    }
}
