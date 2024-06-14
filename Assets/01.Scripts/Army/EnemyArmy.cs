
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

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


        #region MouseEvent

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

        public void OnClick()
        {
            EnemyArmyManager.Instance.OnSelected(this);
        }

        #endregion

        public void OnSelected()
        {
            IsSelected = true;
            OnSelectedOutline();
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


        public Enemy FindNearestEnemy(Transform trm)
        {
            Enemy target = null;
            float maxDistance = 0f;

            foreach (Enemy enemy in Soldiers)
            {
                float distanceToTarget = Vector3.Distance(trm.position, enemy.transform.position);
                if (distanceToTarget > maxDistance)
                {
                    target = enemy;
                    maxDistance = distanceToTarget;
                }
            }//end foreach

            return target;
        }
    }


}