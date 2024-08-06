using UnityEngine;
using UnityEngine.UI;

public class LegionSoldierSlot : MonoBehaviour
{
    [HideInInspector] public Image Icon;
    public bool IsBonus = false;

    private void Awake()
    {
        Icon = transform.Find("Icon").GetComponent<Image>();

        Icon.gameObject.SetActive(false);
    }

    public void SetSlot(EntityInfoDataSO info, string legionName, int legionIdx ,int spawnIdx)
    {
        DummyPenguin dummy = PenguinManager.Instance.SpawnDummyPenguinByInfoData(info);
        dummy.CloneInfo.SetLegionName(legionName);

        Penguin penguin = ArmyManager.Instance.SpawnPenguin(dummy.CloneInfo, spawnIdx);
        PenguinManager.Instance.DummyToPenguinMapping(dummy, penguin);
        ArmyManager.Instance.JoinArmyToSoldier(legionName, legionIdx, penguin);

        Icon.gameObject.SetActive(true);
        Icon.sprite = info.PenguinIcon;
    }
}