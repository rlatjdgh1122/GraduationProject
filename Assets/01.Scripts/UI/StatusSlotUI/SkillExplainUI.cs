using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SkillExplainUI : MonoBehaviour
{
    [SerializeField] private float _panelFadeTime = 0.5f;

    [SerializeField] private TextMeshProUGUI _typeText = null;
    [SerializeField] private TextMeshProUGUI _nameText = null;
    [SerializeField] private TextMeshProUGUI _actionEnterText = null;
    [SerializeField] private TextMeshProUGUI _이후증가 = null;
    [SerializeField] private TextMeshProUGUI _explainText = null;

    private CanvasGroup _panel = null;

    private void Awake()
    {
        _panel = GetComponent<CanvasGroup>();

        _panel.alpha = 0f;
    }

    public void ShowPanel(string skillType, string skillName, string actionEnterText, string explainText)
    {
        _typeText.text = skillType;
        _nameText.text = skillName;

        _actionEnterText.text = actionEnterText;
        if (actionEnterText == "") _이후증가.text = "";
        else _이후증가.text = "이후 사용 가능";

        _explainText.text = explainText;

        _panel.blocksRaycasts = true;
        _panel.DOFade(1, _panelFadeTime).SetUpdate(true).SetEase(Ease.InSine);
    }

    public void HidePanel()
    {
        _panel.blocksRaycasts = false;
        _panel.DOFade(0, _panelFadeTime).SetUpdate(true);
    }
}
