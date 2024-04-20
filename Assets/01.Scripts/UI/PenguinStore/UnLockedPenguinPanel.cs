using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnLockedPenguinPanel : InfoPanel
{
    private Image _penguinBackImg;

    private CanvasGroup _unlockCanvasGroup;
    private Image _unlockIcon;
    private TextMeshProUGUI _unlockText;

    private Button _exitButton;

    public override void Awake()
    {
        base.Awake();

        _penguinBackImg   = transform.Find("PenguinBackImage").GetComponent<Image>();
        _unlockCanvasGroup = transform.Find("Unlock").GetComponent<CanvasGroup>();
        _exitButton = transform.Find("ButtonExit").GetComponent<Button>();
        _unlockIcon = _unlockCanvasGroup.transform.Find("UnlockIcon").GetComponent<Image>();
        _unlockText = _unlockCanvasGroup.transform.Find("NewPenguin").GetComponent<TextMeshProUGUI>();

        _unlockCanvasGroup.alpha = 1;
        penguinFace.color = Color.black;
    }



    public void UnLocked(EntityInfoDataSO infoData)
    {
        PenguinInformataion(infoData);

        ShowPanel();

        UnLockEvent();
    }

    private void UnLockEvent()
    {
        UIManager.Instance.InitializHudTextSequence();

        UIManager.Instance.HudTextSequence.AppendInterval(1f)
            .Append(_unlockIcon.DOFade(0, 1f))
            .Append(_unlockText.DOFade(1, 0.3f))
            .AppendInterval(1.5f)
            .Append(_unlockCanvasGroup.DOFade(0, 0.8f))
            .AppendInterval(0.7f)
            .AppendCallback(() =>
            {
                UpdateSlider(0.7f);
            })
            .Join(penguinFace.DOColor(Color.white, 0.7f))
            .Join(_penguinBackImg.DOFade(0.15f, 0.4f))
            .AppendCallback(() =>
            {
                infoPenguinNameText.DOFade(1f, 0.2f);
                _exitButton.gameObject.SetActive(true);
            });
    }

    public override void HidePanel()
    {
        base.HidePanel();

        ResetData();
    }

    private void ResetData()
    {
        _unlockCanvasGroup.alpha = 1;
        _unlockIcon.DOFade(1, 0f);
        _unlockText.DOFade(0, 0f);
        _penguinBackImg.DOFade(0, 0);
        penguinFace.color = Color.black;
        _exitButton.gameObject.SetActive(false);
        infoPenguinNameText.text = string.Empty;
    }
}
