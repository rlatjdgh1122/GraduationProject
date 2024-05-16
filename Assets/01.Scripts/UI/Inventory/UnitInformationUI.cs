using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitInformationUI : MonoBehaviour
{
    public readonly char Cashing_Separator = ',';

    private EntityInfoDataSO so;

    private TextMeshProUGUI _classNameText;
    private TextMeshProUGUI _nameText;
    private Image _penguinIcon;
    private Image _atkSlide;
    private Image _defSlide;
    private Image _rangeSlide;

    private CanvasGroup _generalInfo;
    private TextMeshProUGUI _synergyText;
    private TextMeshProUGUI _descriptionText;

    private CanvasGroup _detailInfoButton;


    private void Awake()
    {
        _penguinIcon = transform.Find("PenguinFace").GetComponent<Image>();
        _classNameText = transform.Find("PenguinClassTex").GetComponent<TextMeshProUGUI>();
        _nameText = transform.Find("PenguinName").GetComponent<TextMeshProUGUI>();
        _atkSlide = transform.Find("Atk/fillAmount").GetComponent<Image>();
        _defSlide = transform.Find("Def/fillAmount").GetComponent<Image>();
        _rangeSlide = transform.Find("Range/fillAmount").GetComponent<Image>();

        _generalInfo = transform.Find("GeneralInfo").GetComponent<CanvasGroup>();
        _synergyText = _generalInfo.transform.Find("Synergy").GetComponent<TextMeshProUGUI>();
        _descriptionText = _generalInfo.transform.Find("Type").GetComponent<TextMeshProUGUI>();

        _detailInfoButton = transform.Find("DetailInfoButton").GetComponent<CanvasGroup>();
    }

    public void ShowInformation(UnitInventoryData data)
    {
        CleanUpUI();

        if (data == null)
        {
            return;
        }

        SetUIElements(data);

        if (data.InfoData.JobType == PenguinJobType.General)
        {
            ShowGeneralInfo(data.InfoData as GeneralInfoDataSO);
        }
    }

    private void SetUIElements(UnitInventoryData data)
    {
        so = data.InfoData;

        _penguinIcon.gameObject.SetActive(true);
        _penguinIcon.sprite = so.PenguinIcon;

        string jobTypeStr;

        if (so.JobType == PenguinJobType.General)
            jobTypeStr = $"군단장";
        else
            jobTypeStr = $"병사";

        _classNameText.text = jobTypeStr;
        _nameText.text = so.PenguinName;

        _atkSlide.DOFillAmount(so.atk, 0.5f);
        _defSlide.DOFillAmount(so.hp, 0.5f);
        _rangeSlide.DOFillAmount(so.range, 0.5f);

        _detailInfoButton.DOFade(1, 0.5f);
        _detailInfoButton.blocksRaycasts = true;
    }

    private void ShowGeneralInfo(GeneralInfoDataSO generalData)
    {
        _generalInfo.DOFade(1, 0.5f);

        var stat = PenguinManager.Instance.GetGeneralStatToSoliderType(generalData.PenguinType);
        var armyStat = stat.GeneralDetailData.abilities[0];
        var synergyText = generalData.Synergy.Split(Cashing_Separator);

        _synergyText.text = $"{synergyText[0]}<color=green>{armyStat.value}%</color>{synergyText[1]}";

        // _synergyText.text = generalData.Synergy;
        _descriptionText.text = generalData.Description;

        return;
    }

    private void CleanUpUI()
    {
        _penguinIcon.gameObject.SetActive(false);
        _classNameText.text = string.Empty;
        _nameText.text = string.Empty;

        _atkSlide.DOFillAmount(0, 0.5f);
        _defSlide.DOFillAmount(0, 0.5f);
        _rangeSlide.DOFillAmount(0, 0.5f);

        _generalInfo.DOFade(0, 0.2f);
        _synergyText.text = string.Empty;
        _descriptionText.text = string.Empty;

        _detailInfoButton.DOFade(0, 0.5f);
        _detailInfoButton.blocksRaycasts = false;
    }
}