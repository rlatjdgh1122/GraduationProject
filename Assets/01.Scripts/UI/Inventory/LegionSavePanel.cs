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
        _titleText.text = $"{legion}������ �����Ͻðڽ��ϱ�?";
    }

    private void CancelPanel()
    {
        LegionInventoryManager.Instance.UndoLegion(); //���� ���

        UIManager.Instance.ShowWarningUI("���� ���!");

        HideSavePanel();
    }

    private void SavePanel()
    {
        LegionInventoryManager.Instance.SaveLegion(); //�����ϱ�

        UIManager.Instance.ShowWarningUI("���� ����!");

        HideSavePanel();
    }

    public void HideSavePanel()
    {
        UIManager.Instance.HidePanel(this.name);

        _legionChange.ChangeLegion(_legionNumber); //���� �ٲ�� ���ǹ� ����

        _legionNumber = 0;
    }
}
