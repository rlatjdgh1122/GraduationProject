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
    private Coroutine _waitCorou = null;


    public override void Awake()
    {
        base.Awake();

        _buyPanel = transform.Find("BuyLegionPanel").GetComponent<LegionBuyPanel>();
        _buttonText = _changeLegionButon.transform.Find("Text").GetComponent<TextMeshProUGUI>();

        _changeLegionButon.onClick.RemoveAllListeners();
        _changeLegionButon.onClick.AddListener(() => ShowPanel());
    }

    /// <summary>
    /// ���� �ٲٴ� ��ư�� ������ �����
    /// </summary>
    /// <param name="legionNumber">�� �����̴�?</param>
    public void ClickLegionChangeButton(int legionNumber)
    {
        if (legion.LegionInven.ChangedInCurrentLegion()) // ���� ���ܿ� �ٲ�� �ִٸ�
        {
            _savePanel.ShowPanel();
            _savePanel.LegionNumber(legionNumber);
        }
        else
        {
            ChangeLegion(legionNumber);
        }
    }

    public void ChangeLegion(int legionNumber)
    {
        legion.ChangeLegionNumber(legionNumber); //���� ���� �ٲٱ�

        if (legion.LegionList()[legionNumber].Locked) //������ ����ִٸ�
        {
            _buyPanel.ShowPanel();
            _buyPanel.CheckCanBuy(CostManager.Instance.Cost, legion.LegionList()[legionNumber].Price, legionNumber);
        }
        else
        {
            HidePanel();

            legion.LegionInven.ChangeLegion(legion.LegionName(legionNumber));
            ChangeButtonName(legionNumber);
        }
    }


    /// <summary>
    /// ������ ��� ����Ǵ� �͵�
    /// </summary>
    /// <param name="legionNumber">�� �����̴�?</param>
    public void ChangingLegion(int legionNumber)
    {
        _buttonList[legionNumber].UnLocked(); //���� ���� �ر�

        legion.LegionInven.ChangeLegion(legion.LegionName(legionNumber)); //���� �ٲٱ�
        HidePanel(); //�г� �ݱ�

        ChangeButtonName(legionNumber);
    }



    /// <summary>
    /// ������ �ٲ�� ��ư�� Text�� �ٲ���
    /// </summary>
    /// <param name="legionNumber">�� �����̴�?</param>
    public void ChangeButtonName(int legionNumber)
    {
        int textLegion = legionNumber + 1;
        _buttonText.text = $"{textLegion} ����";
    }
}