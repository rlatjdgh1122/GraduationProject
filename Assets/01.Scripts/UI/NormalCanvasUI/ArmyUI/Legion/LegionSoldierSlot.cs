using UnityEngine;
using UnityEngine.UI;

public class LegionSoldierSlot : MonoBehaviour
{
    private Image _icon;
    public bool IsBonus = false;

    private void Awake()
    {
        _icon = transform.Find("Icon").GetComponent<Image>();

        _icon.gameObject.SetActive(false);
    }

    public void SetSlot(EntityInfoDataSO info, int idx)
    {
        DummyPenguin dummy = PenguinManager.Instance.SpawnDummyPenguinByInfoData(info);
        Penguin penguin = ArrangementManager.Instance.SpawnPenguin(dummy.CloneInfo, idx);
        PenguinManager.Instance.DummyToPenguinMapping(dummy, penguin);

        ArmyManager.Instance.JoinArmyToSoldier("1±º´Ü", penguin);
        _icon.gameObject.SetActive(true);
        _icon.sprite = info.PenguinIcon;
    }
}
