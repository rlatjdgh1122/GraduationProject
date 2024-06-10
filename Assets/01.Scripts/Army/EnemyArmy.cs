
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

            if (SoliderCount <= 0)
            {
                EnemyArmyManager.Instance.DeleteArmy(this);
            }

        }

        public void OnMouseEnter()
        {
            foreach (Enemy enemy in Soldiers)
            {
                enemy.OutlineCompo.enabled = true;
                if (enemy.OutlineCompo.isActiveAndEnabled)
                {
                    enemy.OutlineCompo.SetColor(_mouseOverColor);
                }
            }
        }

        public void OnMouseExit()
        {
            if (IsSelected)
            {
                OnSelectedOutline();
            }
            else
            {
                DeSelectedOutline();
               
            }//end else
        }

        public void OnSelected()
        {
            IsSelected = true;

            OnSelectedOutline();
            EnemyArmyManager.Instance.OnSelect(this);
        }

        public void DeSelected()
        {
            IsSelected = false;

            DeSelectedOutline();
        }

        private void OnSelectedOutline()
        {
            foreach (Enemy enemy in Soldiers)
            {
                enemy.OutlineCompo.enabled = true;
                if (enemy.OutlineCompo.isActiveAndEnabled)
                {
                    enemy.OutlineCompo.SetColor(_selectColor);
                }
            }//end foreach
        }

        private void DeSelectedOutline()
        {
            foreach (Enemy enemy in Soldiers)
            {
                enemy.OutlineCompo.enabled = false;
            }//end foreach
        }

    }


}