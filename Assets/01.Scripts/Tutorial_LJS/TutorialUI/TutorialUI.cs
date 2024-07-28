using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private CanvasGroup _tutorialNotification;
    [SerializeField] private TextMeshProUGUI _tutorialText;
    [SerializeField] private CanvasGroup _tutorialInfoUI;

    [SerializeField] private float _fadeDuration;
    [SerializeField] private float _waitTime;

    private TutorialInfoUI _infoUI;

    private void Awake()
    {
        _infoUI = _tutorialInfoUI.GetComponent<TutorialInfoUI>();
    }

    public void ShowPanel(TutorialQuest quest, UnityEvent action = null)
    {
        _infoUI.Init();

        SetTutorialNotification(quest.TutorialTitle);

        foreach (string text in quest.TutorialDescription)
        {
            _infoUI.CreateSlot(text);
        }

        //SoundManager.Play2DSound(SoundName.TutorialStart);
        Fade();
    }

    private void Fade()
    {
        Sequence seq = DOTween.Sequence();

        _tutorialNotification.alpha = 0;
        _tutorialInfoUI.alpha = 0;

        seq.Append(_tutorialNotification.DOFade(1, _fadeDuration))
            .AppendInterval(_waitTime)
            .Append(_tutorialNotification.DOFade(0, _fadeDuration))
            .Append(_tutorialInfoUI.DOFade(1, _fadeDuration));
    }

    private void SetTutorialNotification(string text)
    {
        _tutorialText.text = text;
    }
}