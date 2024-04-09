using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LegionSavePanel : PopupUI
{
    [SerializeField]
    private LegionChange _legionChange;

    private Button _cancelBtn;
    private Button _savelBtn;
    private TextMeshProUGUI _titleText;

    private int _legionNumber;

    public override void Awake()
    {
        base.Awake();

        _cancelBtn = transform.Find("Button/Cancel").GetComponent<Button>();
        _savelBtn  = transform.Find("Button/BuyBtn").GetComponent<Button>();
        _titleText = transform.Find("TitleText").GetComponent<TextMeshProUGUI>();

        _cancelBtn.onClick.AddListener(() => CancelPanel());
        _savelBtn.onClick.AddListener(() => SavePanel());
    }

    public void LegionNumber(int legionNumber)
    {
        Debug.Log(legionNumber);
        _legionNumber = legionNumber;
    }

    public void ShowSavePanel()
    {
        UIManager.Instance.ShowPanel(this.name);

        int legion = LegionInventoryManager.Instance.CurrentLegion + 1;
        _titleText.text = $"{legion}군단을 저장하시겠습니까?";
    }

    private void CancelPanel()
    {
        LegionInventoryManager.Instance.UndoLegion(); //저장 취소

        UIManager.Instance.ShowWarningUI("저장 취소!");

        HideSavePanel();
    }

    private void SavePanel()
    {
        LegionInventoryManager.Instance.SaveLegion(); //저장하기

        UIManager.Instance.ShowWarningUI("저장 성공!");

        HideSavePanel();
    }

    public void HideSavePanel()
    {
        UIManager.Instance.HidePanel(this.name);

        _legionChange.ChangeLegion(_legionNumber); //군단 바뀌는 조건문 실행

        _legionNumber = 0;
    }
}
