
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ArmySystem
{
    [System.Serializable]
    public class EnemyArmy
    {
        public EnemyArmy(List<Enemy> soldiers)
        {
            Soldiers = soldiers;

            //군단 설정
            Soldiers.ForEach(enemy => enemy.JoinEnemyArmy(this));
        }

        public List<Enemy> Soldiers = new();

        public int SoliderCount => Soldiers.Count;
        public bool IsSelected = false;

        private Color _mouseOverColor = Color.white;
        private Color _selectColor = Color.white;

        public void OnChangedOutlineColorHandler(Color overColor, Color selectColor)
        {
            _mouseOverColor = overColor;
            _selectColor = selectColor;
        }

        public void RemoveEnemy(Enemy enemy)
        {
            Soldiers.Remove(enemy);
            enemy.OutlineCompo.enabled = false;
            //enemy.OutlineCompo.SetOutlineMode(Outline.Mode.OutlineHidden);

            if (SoliderCount <= 0)
            {
                EnemyArmyManager.Instance.DeleteArmy(this);
            }

        }

        #region MouseEvent

        public void OnMouseEnter()
        {
            foreach (Enemy enemy in Soldiers)
            {
                enemy.OutlineCompo.enabled = true;
                if (enemy.OutlineCompo.isActiveAndEnabled)
                {
                    //.OutlineCompo.SetOutlineMode(Outline.Mode.OutlineAll);
                    enemy.OutlineCompo.SetColor(_mouseOverColor);
                }
            }
        }

        public void OnMouseExit()
        {
            if (IsSelected)
            {
                OnSelect();
            }
            else
            {
                foreach (Enemy enemy in Soldiers)
                {
                    enemy.OutlineCompo.enabled = false;
                    //enemy.OutlineCompo.SetOutlineMode(Outline.Mode.OutlineHidden);
                }
            }//end else
        }

        public void OnSelect()
        {
            foreach (Enemy enemy in Soldiers)
            {
                enemy.OutlineCompo.enabled = true;
                if (enemy.OutlineCompo.isActiveAndEnabled)
                {
                    //.OutlineCompo.SetOutlineMode(Outline.Mode.OutlineAll);
                    enemy.OutlineCompo.SetColor(_selectColor);
                }
            }
        }

        #endregion

    }


}