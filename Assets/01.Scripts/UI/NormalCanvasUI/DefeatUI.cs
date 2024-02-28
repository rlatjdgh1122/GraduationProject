using System;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefeatUI : NormalUI
{
    private Image _background;
    private CanvasGroup _canvasGroup;
    private TextMeshProUGUI _text;
    private float _elaspedTime => GameManager.Instance.ElapsedTime;

    public override void Awake()
    {
        base.Awake();

        _background = GetComponent<Image>();
        _canvasGroup = GetComponentInChildren<CanvasGroup>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public override void DisableUI(float time, Action action)
    {
        base.DisableUI(time, action);

        DOTween.KillAll(); //임시
        _canvasGroup.DOFade(0, time).OnComplete(() =>
        {
            _background.DOFade(0, time).OnComplete(() => action?.Invoke());
        });
    }

    public override void EnableUI(float time, object obj)
    {
        base.EnableUI(time, obj);

        SetTexts();
        _background.DOFade(0.75f, time).OnComplete(() => _canvasGroup.DOFade(1, time));
    }

    public void SetTexts()
    {
        _text.text = $"경과 시간 : {_elaspedTime.ToString("F0")}초"; //일단 임시임
    }

    public void Restart()
    {
        Debug.Log("다시시작");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
