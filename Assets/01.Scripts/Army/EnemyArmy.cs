
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

            //ó�� ���� ������ ���� Ÿ�� ������
            TargetSoliders = soldiers.ToList();

            //���� ����
            _soldiers.ForEach(enemy => enemy.JoinEnemyArmy(this));

        }

        //�갡 null�̶�� ���� ������ �ȵ� �Ͱ� ���� 
        private Enemy _singleTarget = null;

        //���� ���ܿ� ���ԵǾ� �ִ� ��� ��ϵ��� ��� ����
        public List<Enemy> _soldiers = new();

        //������ Ÿ���� ������ �� �� ����Ʈ�� ����Ͽ� ����
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
            //Ÿ���� �����ϱ��� �ƿ������� �� ���� ��
            DeSelectedOutline();

            if (isSingleTargetMode)
            {
                TargetSoliders.Clear();

                //���� Ÿ������
                TargetSoliders.Add(_singleTarget);

            } //end if
            else
            {
                TargetSoliders.Clear();

                //���� Ÿ������, �������ϱ�
                TargetSoliders = _soldiers.ToList();

            } //end else

            //Ÿ���� �����Ǿ��ٸ� �ٽ� �ƿ������� ����
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