using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NexusUI : NormalUI
{
    [SerializeField] private NexusStat _nexusStat;

    [SerializeField] private TextMeshProUGUI _currentLevel;
    [SerializeField] private TextMeshProUGUI _upgradedLevel;
    [SerializeField] private TextMeshProUGUI _currentHp;
    [SerializeField] private TextMeshProUGUI _upgradedHp;
    [SerializeField] private TextMeshProUGUI _upgradePrice;

    public override void Awake()
    {
        base.Awake();

        UpdateText();
    }

    public void UpdateText()
    {
        _currentLevel.text = $"{_nexusStat.level}";
        _upgradedLevel.text = $"{_nexusStat.level + 1}";
        _currentHp.text = $"{_nexusStat.GetMaxHealthValue()}";
        _upgradedHp.text = $"{_nexusStat.GetUpgradedMaxHealthValue()}";
        _upgradePrice.text = $"{_nexusStat.upgradePrice}";
    }

    public override void ExitButtonUI(float time)
    {
        base.ExitButtonUI(time);
    }

    public override void EnableUI(float time, object obj)
    {
        base.EnableUI(time, obj);

        _cvg.DOFade(1, time);
    }

    public override void DisableUI(float time, Action action)
    {
        _cvg.DOFade(0, time);   
    }
}
