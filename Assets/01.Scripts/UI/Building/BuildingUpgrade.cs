using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingUpgrade : BuildingUIComponent, ICreateSlotUI
{
    [Header("Building Upgrade UI")]
    [SerializeField] private BuildingUpgradeSlot _upgradeSlotPrefab;
    [SerializeField] private List<Color> _colorOfUpgradeList = new();
    [SerializeField] private Transform _upgradeSlotParent;

    private SynergyBuildingInfoDataSO _infoData;
    private List<BuildingUpgradeSlot> _slotList = new();
    private Dictionary<SynergyBuilding, int> _upgradeInfoDic = new();

    private TextMeshProUGUI _woodText;
    private TextMeshProUGUI _stoneText;

    private int _level = 0;

    public override void Awake()
    {
        base.Awake();

        _woodText = transform.Find("NeedWoodCost").GetComponent<TextMeshProUGUI>();
        _stoneText = transform.Find("NeedStoneCost").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        CreateSlot();
    }

    public void CreateSlot()
    {
        for (int i = 0; i < _colorOfUpgradeList.Count; i++)
        {
            var upgradeSlot = Instantiate(_upgradeSlotPrefab, _upgradeSlotParent);
            upgradeSlot.CreateSlot(i, _colorOfUpgradeList[i]);
            _slotList.Add(upgradeSlot);
            upgradeSlot.gameObject.SetActive(false);
        }
    }

    public void OnPurchaseUpgrade()
    {
        _level++;
        UpdateUpgradeInfo();
        UpdateSlot();
        _slotList[_level - 1].OnUnlock();
        UpdateResourceTextForSlot(_level);
    }

    public void OnMovePanel(float x)
    {
        MovePanel(x, 0, panelFadeTime);
    }

    public override void MovePanel(float x, float y, float fadeTime, bool ease = true)
    {
        base.MovePanel(x, y, fadeTime, ease);
        ShowPanel();
    }

    public void InitSlot(SynergyBuildingInfoDataSO data)
    {
        InitializeLevel();

        _infoData = data;

        for (int i = 0; i < _infoData.BuildingAbilityList.Count; i++)
        {
            var ability = data.BuildingAbilityList[i].BuildingUpgradeDescription;
            _slotList[i].gameObject.SetActive(true);
            _slotList[i].Init(ability);
        }

        for (int i = 0; i < _level; i++)
        {
            _slotList[i].OnUnlock();
        }

        UpdateSlot();
        UpdateResourceTextForSlot(_level);
    }

    private void InitializeLevel()
    {
        if (_upgradeInfoDic.TryGetValue(synergyBuilding, out int level))
        {
            _level = level;
        }
        else
        {
            _upgradeInfoDic.Add(synergyBuilding, 0);
            _level = 0;
        }
    }

    public void ClosePanel()
    {
        foreach (var slot in _slotList)
        {
            slot.OnLock();
            slot.gameObject.SetActive(false);
        }
    }

    private void UpdateUpgradeInfo()
    {
        _upgradeInfoDic[synergyBuilding] = _level;
    }

    private void UpdateSlot()
    {
        foreach (var slot in _slotList)
        {
            slot.UpdateSlot();
        }
    }

    private void UpdateResourceTextForSlot(int index)
    {
        var woodResource = _infoData.ReturnNeedResource(index, ResourceType.Wood).NecessaryResourceCount;
        var stoneResource = _infoData.ReturnNeedResource(index, ResourceType.Stone).NecessaryResourceCount;
        UpdateResourceText(_woodText, woodResource);
        UpdateResourceText(_stoneText, stoneResource);
    }

    private void UpdateResourceText(TextMeshProUGUI textComponent, int resourceCount)
    {
        textComponent.gameObject.SetActive(resourceCount >= 0);
        textComponent.text = resourceCount.ToString();
    }
}