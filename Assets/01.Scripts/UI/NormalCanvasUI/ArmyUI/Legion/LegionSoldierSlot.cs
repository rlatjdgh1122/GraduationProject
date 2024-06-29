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

    public void SetSlot(EntityInfoDataSO info)
    {
        _icon.gameObject.SetActive(true);
        _icon.sprite = info.PenguinIcon;
    }
}
