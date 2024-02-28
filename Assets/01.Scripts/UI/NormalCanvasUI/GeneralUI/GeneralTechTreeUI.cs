using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GeneralTechTreeUI : MonoBehaviour
{
    public PenguinStat General;

    #region componenets
    private Image _lockedImage;
    private GeneralUpgradeUI _upgradeUI;
    private Image _generalImage;
    private Button _button;
    private TextMeshProUGUI _levelText;
    private Image _upgradeImage;
    private Button _upgradeButton;
    private CanvasGroup _upgradeElements;
    #endregion

    #region techtree
    public TreeUI[] _treeUI;
    public int num = 0;
    #endregion

    public bool CanUpgrade => _treeUI[num].TreeBoxList.Count == 1;

    private void Awake()
    {
        _lockedImage = transform.Find("lockedImage").GetComponent<Image>();
        _upgradeUI = FindObjectOfType<GeneralUpgradeUI>();
        _generalImage = GetComponent<Image>();
        _levelText = transform.Find("level").GetComponent<TextMeshProUGUI>();
        _upgradeImage = transform.Find("Image").GetComponent<Image>();
        _button = GetComponent<Button>();
        _upgradeButton = transform.Find("upgradeButton").GetComponent<Button>();
        _upgradeElements = transform.Find("upgradeElements").GetComponent<CanvasGroup>();
    }

    public void SetTechTree()
    {
        _treeUI[num].GeneralStat = General;

        _lockedImage.gameObject.SetActive(false);
        _upgradeImage.enabled = true;
        _levelText.enabled = true;
        _levelText.text = $"{General.PenguinData.level}";

        _button.enabled = false;
        _upgradeButton.enabled = true;
        _upgradeButton.onClick.AddListener(() => _upgradeUI.OpenPanel(General));

        _treeUI[num].SetRandom();

        _generalImage.rectTransform.DOAnchorPosX(-425, 1.5f).SetEase(Ease.OutQuint).OnComplete(() =>
        {
            _upgradeElements.DOFade(1, 0.4f);
            _treeUI[num].SetTree();
        });
    }

    public void ContinueTechTree()
    {
        num++;
        _treeUI[num].GeneralStat = General;
        _treeUI[num].SetRandom();
        _treeUI[num].SetTree();
    }
}