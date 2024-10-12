
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
            _soldiers = soldiers;

            //처음 군단 설정할 때도 타겟 정해줌
            TargetSoliders = soldiers.ToList();

            //군단 설정
            _soldiers.ForEach(enemy => enemy.JoinEnemyArmy(this));

        }

        //얘가 null이라면 군단 선택이 안된 것과 같음 
        private Enemy _singleTarget = null;

        //실제 군단에 포함되어 있는 모든 펭귄들을 담고 있음
        public List<Enemy> _soldiers = new();

        //실제로 타겟을 지정할 땐 이 리스트를 사용하여 지정
        public List<Enemy> TargetSoliders = new();

        public int SoliderCount => _soldiers.Count;
        public bool IsSelected = false;

        private Color _mouseOverColor = Color.white;
        private Color _selectColor = Color.white;

        private bool IsNull => _soldiers == null;

        public void OnChangedOutlineColorHandler(Color overColor, Color selectColor)
        {
            _mouseOverColor = overColor;
            _selectColor = selectColor;
        }

        public void RemoveEnemy(Enemy enemy)
        {
            _soldiers.Remove(enemy);

            if (_singleTarget == null && _singleTarget.Equals(enemy))
            {
                _singleTarget = null;

            } //end if

            enemy.OutlineCompo.enabled = false;

            if (SoliderCount <= 0)
            {
                EnemyArmyManager.Instance.DeleteArmy(this);

            } //end if

        }

        public void SetSingleTarget(Enemy enemy)
        {
            _singleTarget = enemy;
        }

        public void SetSingleTargetMode(bool isSingleTargetMode)
        {
            //타겟을 지정하기전 아웃라인을 다 지운 후
            DeSelectedOutline();

            if (isSingleTargetMode)
            {
                TargetSoliders.Clear();

                //단일 타겟지정
                TargetSoliders.Add(_singleTarget);

            } //end if
            else
            {
                TargetSoliders.Clear();

                //군단 타겟지정, 값복사하기
                TargetSoliders = _soldiers.ToList();

            } //end else

            //타겟이 지정되었다면 다시 아웃라인을 켜줌
            OnSelectedOutline();
        }

        #region MouseEvent

        public void OnMouseEnter()
        {
            if (IsSelected) return;

            SetSingleTargetMode(EnemyArmyManager.Instance.IsSingleTargetMode);

            foreach (Enemy enemy in TargetSoliders)
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
            foreach (Enemy enemy in TargetSoliders)
            {
                enemy.OutlineCompo.enabled = true;
                if (enemy.OutlineCompo != null && enemy.OutlineCompo.isActiveAndEnabled)
                {
                    enemy.OutlineCompo.SetColor(_selectColor);
                }
            }//end foreach
        }

        private void DeSelectedOutline()
        {
            foreach (Enemy enemy in TargetSoliders)
            {
                if (enemy.OutlineCompo != null && enemy.OutlineCompo.isActiveAndEnabled)
                {
                    enemy.OutlineCompo.enabled = false;
                }
            }//end foreach
        }

    }


}