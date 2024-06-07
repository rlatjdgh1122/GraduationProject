
using System.Collections.Generic;

namespace ArmySystem
{
    public class EnemyArmy
    {
        public List<Enemy> Soldiers = new();

        public int SoliderCount => Soldiers.Count;

        public void AddEnemy(List<Enemy> enemys)
        {
            Soldiers = enemys;
        }

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

    public class EnemyArmyManager
    {

    }
}