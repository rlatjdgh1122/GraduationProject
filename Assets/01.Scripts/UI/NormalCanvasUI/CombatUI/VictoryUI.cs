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
        _eliminatedEnemy.text = $"óġ�� ���� : {GameManager.Instance.GetCurrentDeadEnemyCount()}����";
        _deadPenguin.text = $"������ �Ʊ� : {GameManager.Instance.GetDeadPenguinCount()}����";
        _reward.text = $"+ {_cost}";
    }

    public void CostViewer()
    {
        CostManager.Instance.AddFromCurrentCost(_cost, true, true, _btn.rectTransform.position);

        WaveManager.Instance.CloseWinPanel();
    }
}
