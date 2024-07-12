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

    public void CreateSlot(int level, Color levelColor)
    {
        _level = level;
        _levelColor = levelColor;
    }

    public void Init(string upgradeDescription)
    {
        _upgradeDescription = upgradeDescription;
    }

    public void UpdateSlot()
    {
        _levelText.color = _levelColor;
        _levelText.text = $"·¹º§ {_level + 1}";
        _upgradeText.text = _upgradeDescription;
    }

    public void OnLock()
    {
        _lockPanel.alpha = 1;
    }

    public void OnUnlock()
    {
        _lockPanel.DOFade(0, 0.5f);
    }
}
