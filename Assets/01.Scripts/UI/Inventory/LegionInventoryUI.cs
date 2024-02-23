using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LegionInventoryUI : MonoBehaviour
{
    #region Public
    [Header("Public Property")]
    public CanvasGroup StatuesMessageImage;
    public TextMeshProUGUI StatuesMessageText;

    #endregion

    #region PenguinInventory

    [Header("Penguin Inventory")]
    [SerializeField] private TextMeshProUGUI _curInventoryName;

    public void OnClickPenguinTypeBtn(string name)
    {
        _curInventoryName.text = name;
    }

    #endregion

    #region LegionInventory

    [Header("Legion Inventory")]
    [SerializeField] private TextMeshProUGUI _curLegionMaxCountTex;
    [SerializeField] private TextMeshProUGUI _curGernalCountInLegionTex;
    [SerializeField] private Color _normalColor;

    public void LegionCountInformation(int i)
    {
        UIManager.Instance.ChangeTextColorBoolean(_curLegionMaxCountTex,
            LegionInventory.Instance.LegionList[i].CurrentCount < LegionInventory.Instance.LegionList[i].MaxCount
            , _normalColor, 
            Color.red,
            0);

        UIManager.Instance.ChangeTextColorBoolean(_curGernalCountInLegionTex, 
            !LegionInventory.Instance.LegionList[i].MaxGereral, 
            _normalColor, 
            Color.red,
            0);

        int maxCount             = LegionInventory.Instance.LegionList[i].MaxCount;
        int curSoliderCount      = LegionInventory.Instance.LegionList[i].CurrentCount;
        int curGeneralCount      = LegionInventory.Instance.LegionList[i].MaxGereral ? 1 : 0;


        _curGernalCountInLegionTex.text     =   $"{curGeneralCount} / 1";
        _curLegionMaxCountTex.text          =   $"{curSoliderCount} / {maxCount}";
    }

    #endregion


    public void ShowMessage(string message) //값들 임시로 박아둔것
    {
        UIManager.Instance.InitializeWarningTextSequence();

        StatuesMessageText.text = message;

        UIManager.Instance.WarningTextSequence.Append(StatuesMessageImage.DOFade(1, 0.04f))
                .AppendInterval(0.8f)
                .Append(StatuesMessageImage.DOFade(0, 0.04f));
    }
}