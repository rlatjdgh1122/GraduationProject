
using System.Collections.Generic;

namespace ArmySystem
{
    public class EnemyArmy
    {
        public List<Enemy> Soldiers = new();

        public int SoliderCount => Soldiers.Count;

        public void AddEnemy(Enemy enemy)
        {
            Soldiers.Add(enemy);
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