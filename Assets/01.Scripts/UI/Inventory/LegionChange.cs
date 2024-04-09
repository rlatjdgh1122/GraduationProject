using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Experimental.GlobalIllumination;
using System.Threading.Tasks;

public class LegionChange : InitLegionChange
{
    [SerializeField]
    private Button _changeLegionButon;
    private TextMeshProUGUI _buttonText;

    private LegionBuyPanel _buyPanel;
    [SerializeField]
    private LegionSavePanel _savePanel;


    public override void Awake()
    {
        base.Awake();

        _buyPanel = transform.Find("BuyLegionPanel").GetComponent<LegionBuyPanel>();
        _buttonText = _changeLegionButon.transform.Find("Text").GetComponent<TextMeshProUGUI>();

        _changeLegionButon.onClick.RemoveAllListeners();
        _changeLegionButon.onClick.AddListener(() => ShowChangeLegionPanel());
    }

    /// <summary>
    /// 군단 바꾸는 버튼을 누르면 실행됨
    /// </summary>
    /// <param name="legionNumber">몇 군단이니?</param>
    public void ClickLegionChangeButton(int legionNumber)
    {
        if (legion.ChangedInCurrentLegion()) // 현재 군단에 바뀐게 있다면
        {
            _savePanel.LegionNumber(legionNumber); //현재 군단을 세이브 패널에 넣어주고
            _savePanel.ShowSavePanel();
        }
        else
        {
            legion.SaveLegion(); //혹시 모르니깐 저장해주고
            ChangeLegion(legionNumber);
        }
    }

    public void ChangeLegion(int legionNumber)
    {
        if (legion.LegionList()[legionNumber].Locked) //군단이 잠겨있다면
        {
            _buyPanel.ShowBuyPanel(); //군단 사는 창 띄우기
            //군단을 살 수 있는지 체크하기
            _buyPanel.CheckCanBuy(CostManager.Instance.Cost, legion.LegionList()[legionNumber].Price, legionNumber);
        }
        else //안잠겨있다면
        {
            HidePanel();

            legion.ChangeLegionNumber(legionNumber); //현재 군단을 선택한 군단 번호로 바꾸기

            legion.ChangeLegion(legionNumber); //현재 군단을 선택한 군단으로 바꾸기
            ChangeButtonName(legionNumber); //버튼 이름을 선택한 군단 번호로 바꾸기
        }
    }


    /// <summary>
    /// 군단을 사면 실행되는 것들
    /// </summary>
    /// <param name="legionNumber">몇 군단이니?</param>
    public void ChangingLegion(int legionNumber)
    {
        _buttonList[legionNumber].UnLocked(); //군단 슬롯 해금

        legion.ChangeLegion(legionNumber); //군단 바꾸기
        HidePanel(); //패널 닫기

        ChangeButtonName(legionNumber);
    }



    /// <summary>
    /// 군단이 바뀌면 버튼의 Text도 바꿔줌
    /// </summary>
    /// <param name="legionNumber">몇 군단이니?</param>
    public void ChangeButtonName(int legionNumber)
    {
        int textLegion = legionNumber + 1;
        _buttonText.text = $"{textLegion} 군단";
    }
}