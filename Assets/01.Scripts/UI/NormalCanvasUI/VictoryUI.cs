using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictoryUI : NormalUI
{
    private Image _background;
    private CanvasGroup _canvasGroup;
    [SerializeField] private TextMeshProUGUI[] _texts;

    public override void Awake()
    {
        _canvasGroup = GetComponentInChildren<CanvasGroup>();
        _background = GetComponent<Image>();
    }

    public override void DisableUI(float time, Action action)
    {
        DOTween.KillAll(); //임시
        _canvasGroup.DOFade(0, time).OnComplete(() =>
        {
            _background.DOFade(0, time).OnComplete(() => action?.Invoke());
        });
    }

    public override void EnableUI(float time, object obj)
    {
        SetTexts();
        _background.DOFade(0.75f, time).OnComplete(() => _canvasGroup.DOFade(1, time));
    }

    private void SetTexts()
    {
        _texts[0].text = $"처치한 적군 : {GameManager.Instance.GetCurrentDeadEnemyCount()}마리";
        _texts[1].text = $"전사한 아군 : {GameManager.Instance.GetDeadPenguinCount()}마리";
    }
}
