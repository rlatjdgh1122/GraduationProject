using ArmySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArmyManager : Singleton<EnemyArmyManager>
{
    public List<EnemyArmy> enemyArmies = new();
    public Color MouseOverColor = Color.white;
    public Color MouseOutColor = Color.white;

    private OnValueChanged<Color> OnChangedOutlineColorEvent = null;

    public EnemyArmy CreateArmy(List<Enemy> enemies)
    {
        var army = new EnemyArmy(enemies);
        enemyArmies.Add(army);

        OnChangedOutlineColorEvent += army.OnChangedOutlineColorHandler;
        OnChangedOutlineColor();

        return army;
    }

    public void DeleteArmy(EnemyArmy army)
    {
        OnChangedOutlineColorEvent -= army.OnChangedOutlineColorHandler;
        enemyArmies.Remove(army);
    }


    public void OnChangedOutlineColor()
    {
        OnChangedOutlineColorEvent?.Invoke(MouseOverColor, MouseOutColor);
    }

    #region MouseEvent

    /// <summary>
    /// hover event
    /// </summary>
    public void OnMouseEnter(EnemyArmy army)
    {
        army.OnMouseEnter();
    }

    /// <summary>
    /// out event
    /// </summary>
    public void OnMouseExit(EnemyArmy army)
    {
        army.OnMouseExit();
    }

    public void OnMouseDown(EnemyArmy army)
    {
        enemyArmies.ObjExcept
            (
                    army,
                    me => army.IsSelected = true,
                    other => other.IsSelected = false
            );// end ObjExcept

        army.OnSelect();
    }

    #endregion


}
