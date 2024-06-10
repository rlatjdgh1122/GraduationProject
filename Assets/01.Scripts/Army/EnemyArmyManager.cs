using ArmySystem;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class EnemyArmyManager : Singleton<EnemyArmyManager>
{
    public List<EnemyArmy> enemyArmies = new();
    public Color MouseOverColor = Color.white;
    public Color MouseOutColor = Color.white;

    private OnValueChanged<Color> OnChangedOutlineColorEvent = null;

    public EnemyArmy CurrnetEnemyArmy { get; private set; } = null;

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

    public void OnSelect(EnemyArmy army)
    {
        CurrnetEnemyArmy = army;

        enemyArmies.ObjExcept //선택된 군단 말곤 다 선택해제해줌
            (
                    army,
                    other => other.DeSelected()
            );// end ObjExcept
    }

    public void DeSelected()
    {
        if (CurrnetEnemyArmy == null) return;

        CurrnetEnemyArmy.DeSelected();

        CurrnetEnemyArmy = null;
    }
}
