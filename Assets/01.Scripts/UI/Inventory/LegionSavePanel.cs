using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class LegionSavePanel : PopupUI
{
    [SerializeField]
    private LegionChange _legionChange;

    private Button _cancelBtn;
    private Button _savelBtn;

    private int _legionNumber;

    public override void Awake()
    {
        base.Awake();

        _cancelBtn = transform.Find("Button/Cancel").GetComponent<Button>();
        _savelBtn = transform.Find("Button/BuyBtn").GetComponent<Button>();

        _cancelBtn.onClick.AddListener(() => CancelPanel());
        _savelBtn.onClick.AddListener(() => SavePanel());
    }

    public void LegionNumber(int legionNumber)
    {
        _legionNumber = legionNumber;
    }

    private void CancelPanel()
    {
        LegionInventoryManager.Instance.LegionInven.UndoLegion();

        HidePanel();
    }

    private void SavePanel()
    {
        LegionInventoryManager.Instance.LegionInven.SaveLegion();

        HidePanel();
    }

    public override void HidePanel()
    {
        base.HidePanel();

        _legionChange.ChangeLegion(_legionNumber);

        _legionNumber = 0;
    }
}
