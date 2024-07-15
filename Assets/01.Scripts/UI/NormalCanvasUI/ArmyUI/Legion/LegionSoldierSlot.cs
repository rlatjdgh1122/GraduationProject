using UnityEngine;
using UnityEngine.UI;

public class LegionSoldierSlot : MonoBehaviour
{
    private Image _icon;
    public bool IsBonus = false;

    public string LegionName { get; set; } = "";

    public int LegionIdx { get; set; } = 0;

    private void Awake()
    {
        _icon = transform.Find("Icon").GetComponent<Image>();

        _icon.gameObject.SetActive(false);
    }

    public void SetSlot(EntityInfoDataSO info, string legionName, int spawnIdx)
    {
        DummyPenguin dummy = PenguinManager.Instance.SpawnDummyPenguinByInfoData(info);
        dummy.CloneInfo.SetLegionName(legionName);

        Penguin penguin = ArmyManager.Instance.SpawnPenguin(dummy.CloneInfo, spawnIdx);
        PenguinManager.Instance.DummyToPenguinMapping(dummy, penguin);
        ArmyManager.Instance.JoinArmyToSoldier(legionName, LegionIdx, penguin);

        _icon.gameObject.SetActive(true);
        _icon.sprite = info.PenguinIcon;
    }
}