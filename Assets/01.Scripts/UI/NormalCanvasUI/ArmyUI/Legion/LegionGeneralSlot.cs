using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LegionGeneralSlot : MonoBehaviour
{
    private Image _icon;

    private void Awake()
    {
        _icon = transform.Find("Icon").GetComponent<Image>();

        _icon.gameObject.SetActive(false);
    }

    public void SetSlot(GeneralInfoDataSO info)
    {
        _icon.gameObject.SetActive(true);
        _icon.sprite = info.PenguinIcon;
    }
}