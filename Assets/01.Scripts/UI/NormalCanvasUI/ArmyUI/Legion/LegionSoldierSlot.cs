using System;
using UnityEngine;
using UnityEngine.UI;

public class LegionSoldierSlot : MonoBehaviour
{
    [SerializeField] private int _index = 0;
    [HideInInspector] public Image Icon;
    public bool IsBonus = false;

    private Health _currentHealth;
    private Penguin _penguin;

    private LegionPanel _parentPanel;

    private void Awake()
    {
        Icon = transform.Find("Icon").GetComponent<Image>();

        var button = transform.Find("Button").GetComponent<Button>();
        button.onClick.AddListener(UpdateSlot);

        _parentPanel = ExtensionMethod.FindParent<LegionPanel>(transform.gameObject);
        Icon.gameObject.SetActive(false);
    }

    public void SetSlot(EntityInfoDataSO info, string legionName, int legionIdx)
    {
        DummyPenguin dummy = PenguinManager.Instance.SpawnDummyPenguinByInfoData(info);
        dummy.CloneInfo.SetLegionName(legionName);

        _penguin = ArmyManager.Instance.SpawnPenguin(dummy.CloneInfo, _index);
        _currentHealth = _penguin.HealthCompo;
        
        PenguinManager.Instance.DummyToPenguinMapping(dummy, _penguin);
        ArmyManager.Instance.JoinArmyToSoldier(legionName, legionIdx, _penguin);

        Icon.gameObject.SetActive(true);
        Icon.sprite = info.PenguinIcon;

        SignalHub.OnBattlePhaseEndEvent += ChangeSlot;
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseEndEvent -= ChangeSlot;
    }

    public void ChangeSlot()
    {
        if (!_currentHealth.IsDead)
            Icon.sprite = _parentPanel.SoldierlInfo.PenguinIcon;
        else
            Icon.sprite = _parentPanel.SkullIcon;
    }

    private void UpdateSlot()
    {
        if (!_currentHealth.IsDead) return;

        _parentPanel.Heal(_penguin, ChangeSlot);
    }
}