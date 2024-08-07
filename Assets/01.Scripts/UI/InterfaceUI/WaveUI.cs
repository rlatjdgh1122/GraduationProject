using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    private void Start()
    {
        _text.text = $"1 웨이브";
    }

    private void OnEnable()
    {
        SignalHub.OnBattlePhaseEndEvent += TextChange;
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseEndEvent -= TextChange;
    }

    private void TextChange()
    {
        _text.text = $"{WaveManager.Instance.CurrentWaveCount + 1} 웨이브";
    }
}