using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingPriceUI : MonoBehaviour
{
    private Image _resourceIcon;
    private TextMeshProUGUI _resourceCount;

    private void Awake()
    {
        _resourceIcon = transform.Find("Icon").GetComponent<Image>();
        _resourceCount = transform.Find("Count").GetComponent<TextMeshProUGUI>();
    }

    public void UpdateUI(Sprite icon, int count)
    {
        _resourceIcon.sprite = icon;
        _resourceCount.text = count.ToString();
    }
}
