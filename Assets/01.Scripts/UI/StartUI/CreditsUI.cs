using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsUI : PopupUI
{
    [Header("Credits")]
    [SerializeField] private float _delayTime;
    [SerializeField] private float _moveDuration;
    [SerializeField] private float _endYPos;

    private Transform _uiTrm;
    private CanvasGroup _exitButton;

    public override void Awake()
    {
        base.Awake();

        _uiTrm = transform.Find("UI").GetComponent<Transform>();
        _exitButton = transform.Find("ExitButton").GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            UIManager.Instance.ShowPanel("CreditUI");
        }
    }

    public override void ShowPanel()
    {
        base.ShowPanel();

        Time.timeScale = 0;

        StartCoroutine(ShowPanelCoroutine());
    }

    public override void HidePanel()
    {
        base.HidePanel();

        Time.timeScale = 1;
    }

    private IEnumerator ShowPanelCoroutine()
    {
        yield return new WaitForSecondsRealtime(_delayTime);

        _uiTrm.DOLocalMoveY(_endYPos, _moveDuration)
            .OnComplete(() =>
            {
                _exitButton.DOFade(1, 0.3f).SetUpdate(true);
                _exitButton.blocksRaycasts = true;
            }).SetUpdate(true);
    }

    public void HideCreditPanel()
    {
        UIManager.Instance.HidePanel("CreditUI");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}