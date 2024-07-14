using ArmySystem;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class MountEventUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public SkillExplainUI ExplainUI = null;

    protected Army Army => ArmyManager.Instance.CurArmy;

    public abstract void OnPointerEnter(PointerEventData eventData);

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        ExplainUI.HidePanel();
    }
}
