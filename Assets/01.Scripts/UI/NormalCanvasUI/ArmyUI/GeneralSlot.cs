using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneralSlot : MonoBehaviour
{
    [SerializeField] private GeneralInfoDataSO _generalInfo;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private Image _icon;

    private void Start()
    {
        SetUI();
    }

    private void SetUI()
    {
        _nameText.text = $"{_generalInfo.PenguinName}";
        _icon.sprite = _generalInfo.PenguinIcon;
    }

    public void OnPurchase()
    {

    }
}
