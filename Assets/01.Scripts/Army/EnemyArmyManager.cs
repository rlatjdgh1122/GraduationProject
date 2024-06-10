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

    public void OnSelected(EnemyArmy army) //���� ����ɶ����� ���⿡ Ÿ���� �־���
    {
        if(army != null)
        {
            CurrnetEnemyArmy = army;

            //���õ� ���� ���� �� ������������
            enemyArmies.ObjExcept 
                (
                        army,
                        other => other.DeSelected()
                );// end ObjExcept
        }//end if
        else //Ÿ���� �������
        {
            foreach(EnemyArmy item in enemyArmies)
            {
                item.DeSelected();
            }
        }
       
    }

    public void DeSelected()
    {
        if (CurrnetEnemyArmy == null) return;

        CurrnetEnemyArmy.DeSelected();

        CurrnetEnemyArmy = null;
    }
}
