using ArmySystem;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LegionNamingPanel : ArmyComponentUI
{
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private Button _legionNameEnterButton;
    [SerializeField] private TextMeshProUGUI _errorMessage;

    public LegionPanel CurrentPanel { get; set; }
    public SoldierSelectPanel ParentPanel { get; set; }

    public Action<LegionPanel> OnLegionNameNamingEvent = null;


    private List<string> _legionNameList = new();
    private string _legionName;
    private bool _canNamingLegion = false;

    public override void Awake()
    {
        base.Awake();

        _inputField.onEndEdit.AddListener(OnValue);
        _legionNameEnterButton.onClick.AddListener(EnterLegionName);
    }

    #region Legion Naming

    //±º´Ü ÀÌ¸§ ¾²°í ¿£ÅÍ ´©¸£°Å³ª È®ÀÎ ¹öÆ° ´­·¶À» ¶§ ±º´Ü ÀÌ¸§ ¿À·ù Ã¼Å©
    private void OnValue(string str)
    {
        if (LegionErrorCheck(str))
            _legionName = str;
    }

    //¿À·ù Ã¼Å©
    private bool LegionErrorCheck(string name)
    {
        if (!TextError(name))
        {
            SetErrorMessage("Æ¯¼ö±âÈ£°¡ Æ÷ÇÔµÇ¾î ÀÖ½À´Ï´Ù!");
            _canNamingLegion = false;
        }
        else if (name.Length < 1)
        {
            SetErrorMessage("ÃÖ¼Ò 1±ÛÀÚ ÀÌ»ó ÀÔ·ÂÇØÁÖ¼¼¿ä!");
            _canNamingLegion = false;
        }
        else if (!DoubleCheck(name))
        {
            SetErrorMessage("Áßº¹µÈ ±º´Ü ÀÌ¸§ÀÌ ÀÖ½À´Ï´Ù!");
            _canNamingLegion = false;
        }
        else
        {
            SetErrorMessage("»ç¿ë °¡´ÉÇÑ ÀÌ¸§ÀÔ´Ï´Ù!");
            _canNamingLegion = true;

            _errorMessage.color = Color.green;
        }

        return _canNamingLegion;
    }

    private void SetErrorMessage(string message)
    {
        _errorMessage.text = message;
    }

    //Æ¯¼ö±âÈ£ Ã¼Å©
    private bool TextError(string text)
    {
        return !Regex.IsMatch(text, @"[^0-9a-zA-Z°¡-ÆR]");
    }

    //Áßº¹ Ã¼Å©
    private bool DoubleCheck(string text)
    {
        return !_legionNameList.Contains(text);
    }

    #endregion

    /// <summary>
    /// ±º´Ü ÀÌ¸§ ¼öÁ¤
    /// </summary>
    /// <param name="currentLegionName"></param>
    public void ModifyLegionName(string currentLegionNumber)
    {
        if (_legionNameList.Contains(currentLegionNumber)) //¸¸¾à ±º´Ü ÀÌ¸§ÀÌ Æ÷ÇÔµÇ¾î ÀÖ´Ù¸é Áö¿öÁØ´Ù
        {
            _legionNameList.Remove(currentLegionNumber);
        }
    }

    public void EnterLegionName()
    {
        if (!_canNamingLegion)
        {
            UIManager.Instance.ShowWarningUI("±º´Ü ÀÌ¸§À» ÁöÁ¤ÇÒ ¼ö ¾ø½À´Ï´Ù!");
            return;
        }

        _legionNameList.Add(_legionName);
        OnLegionNameNamingEvent?.Invoke(CurrentPanel);

        Army army = ArmyManager.Instance.CreateArmy(_legionName);

        CurrentPanel.SetLegionIdx(army.LegionIdx);
        CurrentPanel.SetLegionName(_legionName);

        CurrentPanel.SetLegionData();

        ResetPanel();

        HidePanel();
        ParentPanel.SetActive(true);
    }

    private void ResetPanel()
    {
        _legionName = string.Empty;
        _inputField.text = string.Empty;
        _errorMessage.text = string.Empty;
        _errorMessage.color = Color.red;
    }
}
