using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ArmySystem;
using DG.Tweening;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private RandomComingEnemiesGenerator _randomGenerator;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextMeshProUGUI _enemyText;
    [SerializeField] private TextMeshProUGUI _ourArmyPenguinText;

    private int curWave => WaveManager.Instance.CurrentWaveCount;
    private int _aliveArmyPenguinCount = 0;
    private int _aliveEnemiesCount = 0;

    private void Start()
    {
        _text.text = $"1 웨이브";
        _canvasGroup.alpha = 0;
    }

    private void OnEnable()
    {
        SignalHub.OnBattlePhaseEndEvent += TextChange;
        SignalHub.OnBattlePhaseStartEvent += Battle;

        SignalHub.OnEnemyPenguinDead += EnemyCountUI;
        SignalHub.OnOurPenguinDead += OurArmyCountUI;
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseEndEvent -= TextChange;
        SignalHub.OnBattlePhaseStartEvent -= Battle;

        SignalHub.OnEnemyPenguinDead -= EnemyCountUI;
        SignalHub.OnOurPenguinDead -= OurArmyCountUI;
    }

    private void Battle()
    {
        _canvasGroup.DOFade(1f, 0.5f);

        _aliveArmyPenguinCount = 0;
        _aliveEnemiesCount = _randomGenerator.GetEnemyCount();

        var armies = ArmyManager.Instance.Armies;

        if (curWave == 5 || curWave == 10 || curWave == 15)
            _aliveEnemiesCount++;

        foreach (var army in armies)
        {
            _aliveArmyPenguinCount += army.AlivePenguins.Count;
        }

        EnemyCountUI();
        OurArmyCountUI();
    }

    private void TextChange()
    {
        _text.text = $"{WaveManager.Instance.CurrentWaveCount + 1} 웨이브";

        _canvasGroup.DOFade(0f, 0.5f);
    }

    private void EnemyCountUI()
    {
        int enemyCount = GameManager.Instance.GetCurrentDeadEnemyCount();

        _enemyText.text = $"{_aliveEnemiesCount - enemyCount} | {_aliveEnemiesCount}";
    }

    private void OurArmyCountUI()
    {
        int deadArmyPenguin = GameManager.Instance.GetDeadPenguinCount();

        _ourArmyPenguinText.text = $"{_aliveArmyPenguinCount - deadArmyPenguin} | {_aliveArmyPenguinCount}";
    }
}