using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LegionChangeButton : MonoBehaviour
{
    public int  _legionPrice;

    private TextMeshProUGUI _buttonName;
    private Image  _lockedImge;
    private Button _btn;

    public CanvasGroup canvasGroup { get; set; }

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        _btn = GetComponent<Button>();
        _lockedImge = transform.Find("Locked").GetComponent<Image>();
        _buttonName = transform.Find("Text").GetComponent<TextMeshProUGUI>();

        _btn.onClick.RemoveAllListeners();
    }

    public void CreateBtn(int LegionNumber, int Price)
    {
        transform.name = $"{LegionNumber.ToString()}{transform.name}";
        _legionPrice = Price;

        _buttonName.text = $"{LegionNumber.ToString()} ±º´Ü";

        _btn.onClick.AddListener(() => LegionInventoryManager.Instance.LegionChange.ClickLegionChangeButton(LegionNumber - 1));
    }

    public void UnLocked()
    {
        _lockedImge.gameObject.SetActive(false);
    }
}