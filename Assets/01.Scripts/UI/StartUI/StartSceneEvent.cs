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

    private bool _canClick = true;
    public bool CanClick => _canClick;

    public void GameStartBtn()
    {
        if (!_canClick) return;

        _blackPanel.blocksRaycasts = true;
        _canClick = false;

        _blackPanel.DOFade(1, _fadeDuration)
            .OnComplete(() =>
            {
                LoadingSceneController.LoadScene("IntroCutScene 1-1");
            });
    }

    public void SettingBtn()
    {

    }

    public void ExitBtn()
    {
        Application.Quit();
    }
}
