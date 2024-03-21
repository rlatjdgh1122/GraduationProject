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
    public UnitInformationUI UnitInfoUI;

    #endregion

    #region PenguinInventory

    [Header("Penguin Inventory")]
    [SerializeField] private TextMeshProUGUI _curInventoryName;
    [SerializeField] private Image _selectImg;

    public void OnClickPenguinTypeBtn(string name) //장군 병사 지정 버튼
    {
        _curInventoryName.text = name;
        UnitInfoUI.CleanUpSlot();
        DisableSelectImage();
    }

    ///////선택 이미지////////////
    public void ShowSelectImage(RectTransform trm)
    {
        _selectImg.rectTransform.position = trm.position;
        _selectImg.DOFade(1, 0f);
    }

    public void DisableSelectImage()
    {
        _selectImg.DOFade(0, 0f);
    }
    //////////////////////

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

    #region OnOffLegionInventoryUI
    private CanvasGroup _legionInventory;

    private void Awake()
    {
        _legionInventory = GetComponent<CanvasGroup>();
    }

    public void OnEnableLegionInventory()
    {
        _legionInventory.DOFade(1, 0.5f);
        _legionInventory.blocksRaycasts = true;
    }

    public void OnDisableLegionInventory()
    {
        _legionInventory.DOFade(0, 0.5f);
        DisableSelectImage();
        _legionInventory.blocksRaycasts = false;

        SignalHub.OnModifyArmyInfo?.Invoke();
    }
    #endregion

    public void ShowMessage(string message) //값들 임시로 박아둔것
    {
        UIManager.Instance.InitializHudTextSequence();

        StatuesMessageText.text = message;

        UIManager.Instance.HudTextSequence.Append(StatuesMessageImage.DOFade(1, 0.04f))
                .AppendInterval(0.8f)
                .Append(StatuesMessageImage.DOFade(0, 0.04f));
    }

}