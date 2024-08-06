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

    //���� �̸� ���� ���� �����ų� Ȯ�� ��ư ������ �� ���� �̸� ���� üũ
    private void OnValue(string str)
    {
        if (LegionErrorCheck(str))
            _legionName = str;
    }

    //���� üũ
    private bool LegionErrorCheck(string name)
    {
        if (!TextError(name))
        {
            SetErrorMessage("Ư����ȣ�� ���ԵǾ� �ֽ��ϴ�!");
            _canNamingLegion = false;
        }
        else if (name.Length < 1)
        {
            SetErrorMessage("�ּ� 1���� �̻� �Է����ּ���!");
            _canNamingLegion = false;
        }
        else if (!DoubleCheck(name))
        {
            SetErrorMessage("�ߺ��� ���� �̸��� �ֽ��ϴ�!");
            _canNamingLegion = false;
        }
        else
        {
            SetErrorMessage("��� ������ �̸��Դϴ�!");
            _canNamingLegion = true;

            _errorMessage.color = Color.green;
        }

        return _canNamingLegion;
    }

    private void SetErrorMessage(string message)
    {
        _errorMessage.text = message;
    }

    //Ư����ȣ üũ
    private bool TextError(string text)
    {
        return !Regex.IsMatch(text, @"[^0-9a-zA-Z��-�R]");
    }

    //�ߺ� üũ
    private bool DoubleCheck(string text)
    {
        return !_legionNameList.Contains(text);
    }

    #endregion

    /// <summary>
    /// ���� �̸� ����
    /// </summary>
    /// <param name="currentLegionName"></param>
    public void ModifyLegionName(string currentLegionNumber)
    {
        if (_legionNameList.Contains(currentLegionNumber)) //���� ���� �̸��� ���ԵǾ� �ִٸ� �����ش�
        {
            _legionNameList.Remove(currentLegionNumber);
        }
    }

    public void EnterLegionName()
    {
        if (!_canNamingLegion)
        {
            UIManager.Instance.ShowWarningUI("���� �̸��� ������ �� �����ϴ�!");
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
