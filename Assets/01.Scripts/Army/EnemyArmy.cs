
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ArmySystem
{
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
        }

        #region MouseEvent

        public void OnMouseEnter()
        {
            foreach (Enemy enemy in Soldiers)
            {
                if (enemy.OutlineCompo.isActiveAndEnabled)
                {
                    enemy.OutlineCompo.SetOutlineMode(Outline.Mode.OutlineAll);
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
                    if (enemy.OutlineCompo.isActiveAndEnabled)
                    {
                        enemy.OutlineCompo.SetOutlineMode(Outline.Mode.OutlineHidden);
                    }
                }
            }//end else
        }

        public void OnSelect()
        {
            foreach (Enemy enemy in Soldiers)
            {
                if (enemy.OutlineCompo.isActiveAndEnabled)
                {
                    enemy.OutlineCompo.SetColor(_selectColor);
                }
            }
        }

        #endregion

    }

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

            return army;
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

        #endregion

        public void OnSelectEnemyArmy(EnemyArmy army)
        {
            enemyArmies.ObjExcept
                (
                        army,
                        me => army.IsSelected = true,
                        other => other.IsSelected = false
                );// end ObjExcept

            army.OnSelect();
        }
    }
}