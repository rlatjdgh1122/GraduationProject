using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StartSceneEvent : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _blackPanel;

    [SerializeField]
    private float _fadeDuration;

    private bool _isGameStart = true;

    private bool _canClick => UIManager.Instance.currentPopupUI.Count > 0;

    private void Start()
    {
        Time.timeScale = 1;
    }

    public void GameStartBtn()
    {
        if (!_isGameStart || _canClick) return;

        _blackPanel.blocksRaycasts = true;
        _isGameStart = false;

        _blackPanel.DOFade(1, _fadeDuration)
            .OnComplete(() =>
            {
                LoadingSceneController.LoadScene("IntroCutScene 1-1");
            });
    }

    public void CreditsBtn()
    {
        if (!_isGameStart || _canClick) return;

        UIManager.Instance.ShowPanel("CreditUI");
    }

    public void SettingBtn()
    {
        if (!_isGameStart || _canClick) return;

        UIManager.Instance.ShowPanel("OptionUI");
    }

    public void ExitBtn()
    {
        if (!_isGameStart || _canClick) return;

        Application.Quit();
    }
}
