using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.UI;

public class SkillBarUI : WorldUI
{
    [SerializeField] private Image _skillBar;

    public override void Awake()
    {
        base.Awake();

        SignalHub.OnBattlePhaseStartEvent += BattleStart;
    }

    private void OnDestroy()
    {
        SignalHub.OnBattlePhaseStartEvent -= BattleStart;
    }

    private void BattleStart()
    {
        //��ų ������ Ǯ��
        _skillBar.fillAmount = 1f;
    }

    public void UpdateHpbarUI(float current, float max)
    {
        _skillBar.DOFillAmount(current / max, 0.5f);
    }
}