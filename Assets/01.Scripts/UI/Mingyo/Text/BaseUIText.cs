using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class BaseUIText: MonoBehaviour
{
    protected TextMeshProUGUI _text;
    [SerializeField]
    protected string battlePhaseText;
    [SerializeField]
    protected string remainingPhaseText;

    protected virtual void SetUp()
    {
        Debug.Log(battlePhaseText);
        Debug.Log(remainingPhaseText);

        WaveManager.Instance.OnBattlePhaseStartEvent += () => UpdateText(battlePhaseText);
        WaveManager.Instance.OnBattlePhaseEndEvent += () => UpdateText(remainingPhaseText);
    }

    protected void UpdateText(string text)
    {
        _text.SetText(text);
    }

    protected virtual void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }
}
