using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PenguinInfoUI : PopupUI
{
    protected EntityInfoDataSO _ownerInfoData => PenguinManager.Instance.GetCurrentInfoData;

    [SerializeField] private TextMeshProUGUI _penguinTypeTxt = null;
    [SerializeField] private TextMeshProUGUI _penguinNameTxt = null;
    [SerializeField] private TextMeshProUGUI _penguinDescriptionTxt = null;
    [SerializeField] private TextMeshProUGUI _penguinPersonalityTxt = null;
    [SerializeField] private Image _penguinIcon = null;
    [SerializeField] private TextMeshProUGUI _legionNameTxt = null;

    protected virtual void ShowInfo()
    {
        _penguinPersonalityTxt.text = _ownerInfoData.PenguinPersonality;
        _penguinDescriptionTxt.text = _ownerInfoData.PenguinDescription;
        _penguinTypeTxt.text = _ownerInfoData.PenguinTypeName;
        _penguinNameTxt.text = _ownerInfoData.PenguinName;
        _penguinIcon.sprite = _ownerInfoData.PenguinIcon;
        _legionNameTxt.text = _ownerInfoData.LegionName;
    }

    public virtual void HideInfoUI()
    {
        UIManager.Instance.HidePanel("PenguinInfoUI");
    }

    public override void HidePanel()
    {
        base.HidePanel();

        _rectTransform.DOScale(Vector3.zero, 0.5f);
        PenguinManager.Instance.DummyPenguinList.ForEach(p => p.OutlineCompo.enabled = false);
        PenguinManager.Instance.DummyPenguinCameraCompo.DisableCamera();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();

        UIManager.Instance.HidePanel("StorePanel");

        _rectTransform.DOScale(Vector3.one, 0.9f);
        ShowInfo();
    }
}
