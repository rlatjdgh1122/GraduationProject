using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LegionCountUI : MonoBehaviour
{
    private TextMeshProUGUI _generalText;
    private TextMeshProUGUI _soliderText;

    private void Awake()
    {
        _generalText = transform.Find("GeneralCount").GetComponent<TextMeshProUGUI>();
        _soliderText = transform.Find("SoliderCount").GetComponent<TextMeshProUGUI>();
    }

    public void LegionCountSetting(int _general, int _solider, int _soliderMax)
    {
        _generalText.text = $"{_general} / 1";
        _soliderText.text = $"{_solider} / {_soliderMax}";
    }
}
