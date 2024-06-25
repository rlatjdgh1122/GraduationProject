using DG.Tweening;
using UnityEngine.UI;

public class SoldierInfo : PopupUI
{
    private EntityInfoDataSO _infoSO;
    private Image _atkSlide;
    private Image _defSlide;

    public override void Awake()
    {
        base.Awake();

        _atkSlide = transform.Find("AtkFill").GetComponent<Image>();
        _defSlide = transform.Find("DefFill").GetComponent<Image>();
    }

    public void SetInfo(EntityInfoDataSO info)
    {
        _infoSO = info;
    }

    public override void ShowPanel()
    {
        base.ShowPanel();

        _atkSlide.DOFillAmount(_infoSO.atk, 0.5f);
        _defSlide.DOFillAmount(_infoSO.hp, 0.5f);
    }

    public override void HidePanel()
    {
        base.HidePanel();

        _atkSlide.DOFillAmount(0, 0.5f);
        _defSlide.DOFillAmount(0, 0.5f);
    }
}
