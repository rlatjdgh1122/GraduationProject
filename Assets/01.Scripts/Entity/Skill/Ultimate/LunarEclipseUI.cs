using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LunarEclipseUI : MonoBehaviour
{
    [SerializeField]
    private Color _beforeGlow;

    [SerializeField]
    private Color _afterGlow = Color.red;

    [SerializeField]
    private float _localMoveXValue;

    [SerializeField] private Image _glowImge;
    [SerializeField] private Image _blackMoonImage;
    [SerializeField] private Image _redMoonImage;

    private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
            StartEclipse(2f);
        if (Input.GetKeyDown(KeyCode.X))
            EndEclipse(1f);
    }

    public void StartEclipse(float duration)
    {
        UIManager.Instance.InitializHudTextSequence();

        UIManager.Instance.HudTextSequence
            .Append(_canvasGroup.DOFade(1, 0.5f))
            .Join(_blackMoonImage.transform.DOLocalMoveX(0, duration))
            .Join(_blackMoonImage.DOFade(1, 0.5f))
            .Join(_glowImge.DOColor(_afterGlow, 3.5f))
            .Append(_redMoonImage.DOFade(1, duration));
    }

    public void EndEclipse(float duration)
    {
        UIManager.Instance.InitializHudTextSequence();

        UIManager.Instance.HudTextSequence
            .Append(_redMoonImage.DOFade(0, duration))
            .Join(_blackMoonImage.DOFade(0, 0.5f))
            .Join(_blackMoonImage.transform.DOLocalMoveX(_localMoveXValue, duration))
            .Join(_glowImge.DOColor(_beforeGlow, 3.5f))
            .Append(_canvasGroup.DOFade(0, 0.5f));
    }
}
