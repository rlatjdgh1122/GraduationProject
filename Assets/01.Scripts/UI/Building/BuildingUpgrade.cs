using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//나중에 업그레이드 관리하는거 따로 빼야함 일단 기능 만들어놓음
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

    [SerializeField] private TextMeshProUGUI _buildingNameText;

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
        if (buildingHealth.IsDead)
        {
            UIManager.Instance.ShowWarningUI("건물이 부서졌습니다!");
            return;
        }

        if (_level >= _infoData.BuildingAbilityList.Count) return;

        if (!ResourceManager.Instance.CheckAllResources(_infoData[_level].UpgradePriceArr))
        {
            UIManager.Instance.ShowWarningUI("자원이 부족합니다!");
            return;
        }

        ResourceManager.Instance.RemoveResource(_infoData[_level].UpgradePriceArr, () =>
        {
            OnClickEvent();
        });
    }

    private void OnClickEvent()
    {
        _level++;

        UpdateUpgradeInfo();
        UpdateSlot();
        _slotList[_level - 1].OnUnlock();
        UpdateResourceTextForSlot(_level);

        synergyBuilding.SetSynergyBuff(_infoData[_level - 1].BuildingAbility);
        synergyBuilding.LevelUpHealingTime(_infoData[_level - 1].ReduceHealingTime);
        synergyBuilding.LevelUpSkillTime(_infoData[_level - 1].ReduceSkillTime);
    }

    public void OnMovePanel(float x)
    {
        MovePanel(x, 0, panelFadeTime);
    }

    public void InitSlot(SynergyBuildingInfoDataSO data)
    {
        InitializeLevel();

        _infoData = data;

        for (int i = 0; i < _infoData.BuildingAbilityList.Count; i++)
        {
            var ability = data[i].BuildingUpgradeDescription;
            _slotList[i].gameObject.SetActive(true);
            _slotList[i].Init(ability);
        }

        if (!buildingHealth.IsDead)
        {
            for (int i = 0; i < _level; i++)
            {
                _slotList[i].OnUnlock();
            }
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

    private void UpdateResourceTextForSlot(int index) //만약 자원 종류를 더 추가한다면 이 부분 바꿔야함
    {
        if (index >= _infoData.BuildingAbilityList.Count) return;

        foreach (var data in _infoData[index].UpgradePriceArr)
        {
            if (data.Type == ResourceType.Stone)
            {
                UpdateResourceText(_stoneText, data.Count);
            }
            else
            {
                UpdateResourceText(_woodText, data.Count);
            }
        }
    }

    private void UpdateResourceText(TextMeshProUGUI textComponent, int resourceCount)
    {
        textComponent.gameObject.SetActive(resourceCount >= 0);
        textComponent.text = resourceCount.ToString();
    }

    public override void ShowPanel()
    {
        base.ShowPanel();

        BuildingNameText(_infoData.BuildingName);
    }

    public void BuildingNameText(string buildingName)
    {
        string brokenText = string.Empty;

        brokenText = buildingHealth.IsDead == true ? "부서진 " : string.Empty;

        _buildingNameText.text = $"{brokenText}{buildingName}";
    }
}