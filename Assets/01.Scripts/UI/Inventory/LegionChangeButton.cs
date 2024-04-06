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

    private void Awake()
    {
        _btn = GetComponent<Button>();
        _lockedImge = transform.Find("Locked").GetComponent<Image>();
        _buttonName = transform.Find("Text").GetComponent<TextMeshProUGUI>();

        _btn.onClick.RemoveAllListeners();

        //_btn.onClick.AddListener(() => );
    }

    public void CreateBtn(int LegionNumber, int Price)
    {
        transform.name = $"{LegionNumber.ToString()}{transform.name}";
        _legionPrice = Price;

        _buttonName.text = $"{LegionNumber.ToString()} ±º´Ü";
    }

    public void ChangeButtonName(string name)
    {
        _buttonName.text = name;
    }

    public void UnLocked()
    {
        _lockedImge.gameObject.SetActive(true);
    }
}