using ArmySystem;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LegionNamingPanel : ArmyComponentUI
{
    // [SerializeField] private TMP_InputField _inputField;
    //[SerializeField] private Button _legionNameEnterButton;
    //[SerializeField] private TextMeshProUGUI _errorMessage;

    public LegionPanel CurrentPanel { get; set; }
    public SoldierSelectPanel ParentPanel { get; set; }

    public Action<LegionPanel> OnLegionNameNamingEvent = null;


    private List<string> _legionNameList = new();
    public string LegionName;
    private bool _canNamingLegion = false;

    public override void Awake()
    {
        base.Awake();

        //  _inputField.onEndEdit.AddListener(OnValue);
        // _legionNameEnterButton.onClick.AddListener(EnterLegionName);
    }

    #region Legion Naming

    //군단 이름 쓰고 엔터 누르거나 확인 버튼 눌렀을 때 군단 이름 오류 체크
   /* private void OnValue(string str)
    {
        if (LegionErrorCheck(str))
        {
            LegionName = str;

        }
    }

    //오류 체크
    private bool LegionErrorCheck(string name)
    {
        if (!TextError(name))
        {
            SetErrorMessage("특수기호가 포함되어 있습니다!");
            _canNamingLegion = false;
        }
        else if (name.Length < 1)
        {
            SetErrorMessage("최소 1글자 이상 입력해주세요!");
            _canNamingLegion = false;
        }
        else if (!DoubleCheck(name))
        {
            SetErrorMessage("중복된 군단 이름이 있습니다!");
            _canNamingLegion = false;
        }
        else
        {
            SetErrorMessage("사용 가능한 이름입니다!");
            _canNamingLegion = true;

            _errorMessage.color = Color.green;
        }

        return _canNamingLegion;
    }

    private void SetErrorMessage(string message)
    {
        _errorMessage.text = message;
    }

    //특수기호 체크
    private bool TextError(string text)
    {
        return !Regex.IsMatch(text, @"[^0-9a-zA-Z가-힣]");
    }

    //중복 체크
    private bool DoubleCheck(string text)
    {
        return !_legionNameList.Contains(text);
    }*/

    #endregion

    /// <summary>
    /// 군단 이름 수정
    /// </summary>
    /// <param name="currentLegionName"></param>
    public void ModifyLegionName(string currentLegionNumber)
    {
        if (_legionNameList.Contains(currentLegionNumber)) //만약 군단 이름이 포함되어 있다면 지워준다
        {
            _legionNameList.Remove(currentLegionNumber);
        }
    }

    public void EnterLegionName()
    {
        if (!_canNamingLegion)
        {
            UIManager.Instance.ShowWarningUI("군단 이름을 지정할 수 없습니다!");
            return;
        }

        _legionNameList.Add(LegionName);

       // Army army = ArmyManager.Instance.CreateArmy(LegionName);

        //CurrentPanel.SetLegionIdx(army.LegionIdx);
        CurrentPanel.SetLegionName(LegionName);

        CurrentPanel.SetLegionData();

        ResetPanel();

        HidePanel();
        ParentPanel.SetActive(true);

        OnLegionNameNamingEvent?.Invoke(CurrentPanel);
    }

    private void ResetPanel()
    {
        _canNamingLegion = false;
        LegionName = string.Empty;

        /*_inputField.text = string.Empty;
        _errorMessage.text = string.Empty;
        _errorMessage.color = Color.red;*/
    }
}
