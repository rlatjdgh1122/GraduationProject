using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LegionChange : MonoBehaviour
{
    [SerializeField] private float _changeTime = 0.2f;

    [SerializeField] private TextMeshProUGUI _curLegionNumberTex;
        
    [SerializeField] private Image _backPanel;
    [SerializeField] private Transform _legionNumberTrm;

    private CanvasGroup[] _legionButtons;

    private void Start()
    {
        _legionButtons = _legionNumberTrm.GetComponentsInChildren<CanvasGroup>();
    }

    public void ChangeCurrentLegionNumber(string text)
    {
        _curLegionNumberTex.text = $"{text} ±º´Ü";
    }

    public void SelectLegionNumber(int number)
    {
        string text = $"{number}";

        ChangeCurrentLegionNumber(text);

        SelectButton();
    }

    public void ChangeButton()
    {
        UIManager.Instance.InitializeWarningTextSequence();

        _backPanel.DOFade(0.5f, _changeTime);

        for(int i = 0; i < _legionButtons.Length; i++)
        {
            UIManager.Instance.WarningTextSequence
                .Append(_legionButtons[i].DOFade(1, _changeTime));
            _legionButtons[i].blocksRaycasts = true;
        }
    }

    private void SelectButton()
    {
        UIManager.Instance.InitializeWarningTextSequence();

        _backPanel.DOFade(0f, _changeTime);

        for (int i = _legionButtons.Length - 1; i >= 0; i--)
        {
            UIManager.Instance.WarningTextSequence
                .Append(_legionButtons[i].DOFade(0, _changeTime));
            _legionButtons[i].blocksRaycasts = false;
        }
    }
}