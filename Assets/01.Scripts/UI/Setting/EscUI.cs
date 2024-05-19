using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EscUI : PopupUI
{
    [Header("ESC")]
    [SerializeField]
    private float _buttonFadeTime = 0.2f;

    [SerializeField]
    private float _screenPos = 50;

    private CanvasGroup[] _buttons;

    protected bool _canHide = true;

    private float _currentTimeScale = 0;

    public override void Awake()
    {
        base.Awake();

        _buttons = transform.Find("Buttons").GetComponentsInChildren<CanvasGroup>();
    }

    public override void ShowPanel()
    {
        _canHide = false;

        _currentTimeScale = Time.timeScale;
        Time.timeScale = 0;

        base.ShowPanel();

        ShowButton();
    }

    public override void HidePanel()
    {
        if (!_canHide) return;
        HideButton();

        Time.timeScale = _currentTimeScale;

        base.HidePanel();
    }

    private void ShowButton()
    {
        UIManager.Instance.InitializHudTextSequence();

        UIManager.Instance.HudTextSequence.PrependInterval(0.8f);

        foreach(var button in _buttons)
        {
            UIManager.Instance.HudTextSequence
               .Append(button.DOFade(1, _buttonFadeTime))
               .Join(button.transform.DOMoveY(button.transform.position.y + _screenPos, _buttonFadeTime))
               .SetEase(Ease.OutSine).SetUpdate(true);
            button.blocksRaycasts = true;
        }

        StartCoroutine(WaitAndSetShowingFalse(_buttonFadeTime * _buttons.Length + 1f));
    }
    private IEnumerator WaitAndSetShowingFalse(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);
        _canHide = true;
    }

    private void HideButton()
    {
        foreach(var button in _buttons)
        {
            button.alpha = 0;
            button.transform.DOMoveY(button.transform.position.y - _screenPos, 0).SetUpdate(true);
            button.blocksRaycasts = false;
        }

    }
}