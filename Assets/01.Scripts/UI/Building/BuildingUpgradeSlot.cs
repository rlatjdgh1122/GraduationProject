using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildingUpgradeSlot : MonoBehaviour
{
    private int _level;
    private Color _levelColor;
    private string _upgradeDescription;

    private CanvasGroup _lockPanel;

    private TextMeshProUGUI _levelText;
    private TextMeshProUGUI _upgradeText;

    private void Awake()
    {
        _lockPanel = transform.Find("Panel/LockPanel").GetComponent<CanvasGroup>();

        _levelText = transform.Find("Panel/LevelText").GetComponent<TextMeshProUGUI>();
        _upgradeText = transform.Find("Panel/SynergyDescript").GetComponent<TextMeshProUGUI>();
    }

    public void Init(int level, Color levelColor, string upgradeDescription)
    {
        _level = level;
        _levelColor = levelColor;
        _upgradeDescription = upgradeDescription;
    }

    public void UpdateSlot()
    {
        _levelText.color = _levelColor;
        _levelText.text = $"{_level + 1}";
        _upgradeText.text = _upgradeDescription;
    }

    public void OnUnlock()
    {
        _lockPanel.DOFade(0, 0.5f);
    }
}
