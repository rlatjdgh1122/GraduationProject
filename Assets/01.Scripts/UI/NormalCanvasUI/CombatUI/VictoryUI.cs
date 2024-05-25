using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VictoryUI : PopupUI
{
    [SerializeField] private TextMeshProUGUI _eliminatedEnemy;
    [SerializeField] private TextMeshProUGUI _deadPenguin;
    [SerializeField] private TextMeshProUGUI _reward;
    [SerializeField] private SoundName VictorySound;
    [SerializeField] private int _cost;
    [SerializeField] private Image _btn;

    public override void Awake()
    {
        base.Awake();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();

        SoundManager.Play2DSound(VictorySound);
        SetTexts();
    }

    public override void HidePanel()
    {
        base.HidePanel();
    }

    private void SetTexts()
    {
        _eliminatedEnemy.text = $"처치한 적군 : {GameManager.Instance.GetCurrentDeadEnemyCount()}마리";
        _deadPenguin.text = $"전사한 아군 : {GameManager.Instance.GetDeadPenguinCount()}마리";
        _reward.text = $"+ {_cost}";
    }

    public void CostViewer()
    {
        CostManager.Instance.AddFromCurrentCost(_cost, true, true, _btn.rectTransform.position);

        WaveManager.Instance.CloseWinPanel();
    }
}
