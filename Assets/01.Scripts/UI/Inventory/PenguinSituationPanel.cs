using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PenguinSituationPanel : PopupUI
{
    [SerializeField]
    private CanvasGroup _healPanel, _retirePanel;

    [SerializeField]
    private TextMeshProUGUI _situationText;

    public void ShowHealPanel()
    {
        ChangingPanel(_retirePanel, _healPanel);


    }

    public void ShowRetirePanel()
    {
        ChangingPanel(_healPanel, _retirePanel);


    }

    private void ChangingPanel(CanvasGroup beforePanel, CanvasGroup afterPanel)
    {
        beforePanel.alpha = 0;
        beforePanel.blocksRaycasts = false;

        afterPanel.DOFade(1, 0.4f);
        afterPanel.blocksRaycasts = true;
    }

    private void SetSituationText(string text)
    {
        _situationText.text = text;
    }
}
