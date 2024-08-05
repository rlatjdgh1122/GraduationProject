using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialArmyUseSkill : MonoBehaviour
{
    [SerializeField] private Image _skillImage;
    [SerializeField] private Image _ultimateImage;

    [SerializeField] private Sprite _lockIcon;
    [SerializeField] private Sprite _skillIcon;
    [SerializeField] private Sprite _ultimateIcon;

    public void Lock()
    {
        _skillImage.sprite = _lockIcon;
        _ultimateImage.sprite = _lockIcon;
    }

    public void ShowSkill()
    {
        _skillImage.sprite = _skillIcon;
    }

    public void ShowUltimate()
    {
        _ultimateImage.sprite = _ultimateIcon;
    }
}
