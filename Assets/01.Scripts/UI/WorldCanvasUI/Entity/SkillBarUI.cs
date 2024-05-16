using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class SkillBarUI : WorldUI
{ 
    [SerializeField] private Image _skillBar;

    public void UpdateHpbarUI(float current, float max)
    {
        _skillBar.DOFillAmount(current / max, 0.5f);
    }
}