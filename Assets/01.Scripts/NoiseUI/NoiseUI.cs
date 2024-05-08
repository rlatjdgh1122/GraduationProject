using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoiseUI : MonoBehaviour
{
    [SerializeField]
    private float _fadeNoiseUIDuration;

    [SerializeField]
    private float _fillamountFadeDuration;

    [SerializeField]
    private float _noiseShakeDuration;

    [SerializeField]
    private float _noiseShakeIncrease;

    private Image _viewNoiseFillImage;
    private Image _noiseFillImage;
    private CanvasGroup _canvasGroup;
    private Transform _noisePanel;

    private NoiseManager _noiseManager => NoiseManager.Instance;

    private void Awake()
    {
        Transform trm = transform.Find("MainPanel/FillAmountPanel");
        _viewNoiseFillImage = trm.Find("ViewNoiseFillAmount").GetComponent<Image>();
        _noiseFillImage = trm.Find("NoiseFillAmount").GetComponent<Image>();
        _canvasGroup = GetComponent<CanvasGroup>();
        _noisePanel = transform.Find("MainPanel").GetComponent<Transform>();
    }

    private void OnEnable()
    {
        SignalHub.OnViewNoiseIncreaseEvent += IncreaseViewNoiseUI;
        SignalHub.OnViewNoiseIncreaseEvent += WarningNoiseUI;
        SignalHub.OnNoiseIncreaseEvent += IncreaseNoiseUI;
        SignalHub.OnBattlePhaseStartEvent += HideNoiseUI;
        SignalHub.OnBattlePhaseEndEvent += ShowNoiseUI;
    }

    private void OnDisable()
    {
        SignalHub.OnViewNoiseIncreaseEvent -= IncreaseViewNoiseUI;
        SignalHub.OnViewNoiseIncreaseEvent -= WarningNoiseUI;
        SignalHub.OnNoiseIncreaseEvent -= IncreaseNoiseUI;
        SignalHub.OnBattlePhaseStartEvent -= HideNoiseUI;
        SignalHub.OnBattlePhaseEndEvent -= ShowNoiseUI;
    }

    private void IncreaseViewNoiseUI()
    {
        _viewNoiseFillImage.DOFillAmount(_noiseManager.ViewNoisePercent, _fillamountFadeDuration);
    }

    private void IncreaseNoiseUI()
    {
        _noiseFillImage.DOFillAmount(_noiseManager.NoisePercent, _fillamountFadeDuration);
    }

    private void WarningNoiseUI()
    {
        _noisePanel.DOShakePosition(_noiseShakeDuration, _noiseManager.ViewNoisePercent * _noiseShakeIncrease);
    }

    public void ShowNoiseUI()
    {
        _canvasGroup.DOFade(1, _fadeNoiseUIDuration);
    }

    public void HideNoiseUI()
    {
        _canvasGroup.DOFade(0, _fadeNoiseUIDuration).OnComplete(() => ResetUI());
    }

    public void ResetUI()
    {
        _viewNoiseFillImage.fillAmount = 0;
        _noiseFillImage.fillAmount = 0;
    }
}