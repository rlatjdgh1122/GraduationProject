using System;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefeatUI : PopupUI
{
    private Image _background;
    private CanvasGroup _canvasGroup;
    private TextMeshProUGUI _text;
    private float _elaspedTime => GameManager.Instance.ElapsedTime;

    public override void Awake()
    {
        _background = GetComponent<Image>();
        _canvasGroup = GetComponentInChildren<CanvasGroup>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public override void DisableUI(float time, Action action)
    {
        DOTween.KillAll(); //�ӽ�
        _canvasGroup.DOFade(0, time).OnComplete(() =>
        {
            _background.DOFade(0, time).OnComplete(() => action?.Invoke());
        });
    }

    public override void EnableUI(float time)
    {
        SetTexts();
        _background.DOFade(0.75f, time).OnComplete(() => _canvasGroup.DOFade(1, time));
    }

    public void SetTexts()
    {
        _text.text = $"��� �ð� : {_elaspedTime.ToString("F0")}��"; //�ϴ� �ӽ���
    }

    public void Restart()
    {
        Debug.Log("�ٽý���");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}