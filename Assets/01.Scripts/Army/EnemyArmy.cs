
using System.Collections.Generic;
using UnityEngine;

namespace ArmySystem
{
    public class EnemyArmy
    {
        public EnemyArmy(List<Enemy> soldiers)
        {
            Soldiers = soldiers;
        }

        public List<Enemy> Soldiers = new();

        public int SoliderCount => Soldiers.Count;


        public void RemoveEnemy(Enemy enemy)
        {
            Soldiers.Remove(enemy);
        }

        public void ShowOutline()
        {

        }

        public void HideOutline()
        {

        }

    }

    public class EnemyArmyManager : MonoBehaviour
    {
        public List<EnemyArmy> enemyArmies = new();

        public EnemyArmy CreateArmy(List<Enemy> enemies)
        {
            var army = new EnemyArmy(enemies);
            enemyArmies.Add(army);

            return army;
        }

        public void Hover(EnemyArmy army)
        {
            army.HideOutline();
        }

        public void Click()
        {

        }
    }
}