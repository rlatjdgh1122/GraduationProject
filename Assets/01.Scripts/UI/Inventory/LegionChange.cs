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
    /// ���� �ٲٴ� ��ư�� ������ �����
    /// </summary>
    /// <param name="legionNumber">�� �����̴�?</param>
    public void ClickLegionChangeButton(int legionNumber)
    {
        if (legion.ChangedInCurrentLegion()) // ���� ���ܿ� �ٲ�� �ִٸ�
        {
            _savePanel.LegionNumber(legionNumber); //���� ������ ���̺� �гο� �־��ְ�
            _savePanel.ShowSavePanel();
        }
        else
        {
            legion.SaveLegion(); //Ȥ�� �𸣴ϱ� �������ְ�
            ChangeLegion(legionNumber);
        }
    }

    public void ChangeLegion(int legionNumber)
    {
        if (legion.LegionList()[legionNumber].Locked) //������ ����ִٸ�
        {
            _buyPanel.ShowBuyPanel(); //���� ��� â ����
            //������ �� �� �ִ��� üũ�ϱ�
            _buyPanel.CheckCanBuy(CostManager.Instance.Cost, legion.LegionList()[legionNumber].Price, legionNumber);
        }
        else //������ִٸ�
        {
            HidePanel();

            legion.ChangeLegionNumber(legionNumber); //���� ������ ������ ���� ��ȣ�� �ٲٱ�

            legion.ChangeLegion(legionNumber); //���� ������ ������ �������� �ٲٱ�
            ChangeButtonName(legionNumber); //��ư �̸��� ������ ���� ��ȣ�� �ٲٱ�
        }
    }


    /// <summary>
    /// ������ ��� ����Ǵ� �͵�
    /// </summary>
    /// <param name="legionNumber">�� �����̴�?</param>
    public void ChangingLegion(int legionNumber)
    {
        _buttonList[legionNumber].UnLocked(); //���� ���� �ر�

        legion.ChangeLegion(legionNumber); //���� �ٲٱ�
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