using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class VictoryUI : PopupUI
{
    private CanvasGroup _canvasGroup;
    [SerializeField] private TextMeshProUGUI[] _texts;

    public override void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    public override void DisableUI(float time, Action action)
    {
        _canvasGroup.DOFade(0, time).OnComplete(() =>  action?.Invoke());
    }

    public override void EnableUI(float time)
    {
        SetTexts();
        _canvasGroup.DOFade(1, time);
    }

    private void SetTexts()
    {
        _texts[0].text = $"처치한 적군 : {GameManager.Instance.GetCurrentDeadEnemyCount()}마리";
        _texts[1].text = $"전사한 아군 : {GameManager.Instance.GetDeadPenguinCount()}마리";
    }
}
