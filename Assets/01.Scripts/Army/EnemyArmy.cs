
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
        public bool IsMouseOver = false;

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
            TargetSoliders.Remove(enemy);

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

        public void OnUpdatedSingleTargetMode(bool isSingleTargetMode)
        {
            //Ÿ���� �����ϱ��� �ƿ������� �� ���� ��
            DeSelectedOutline();

            if (isSingleTargetMode)
            {
                TargetSoliders.Clear();

                //���� Ÿ������
                if (_singleTarget != null)
                    TargetSoliders.Add(_singleTarget);

            } //end if
            else
            {
                TargetSoliders.Clear();

                //���� Ÿ������, �������ϱ�
                TargetSoliders = _soldiers.ToList();

            } //end else

            if (IsSelected) //Ÿ���� ������ ��Ȳ�̶��
            {
                OnSelectedOutline();
            } //end if

            else if (IsMouseOver) //���콺�� ������ ��Ȳ�̶��
            {
                OnMouseOverOutline();
            } //end else if

            else //�ƹ��͵� �ƴ϶��
            {
                DeSelectedOutline();
            } //end else
        }

        #region MouseEvent

        public void OnMouseEnter()
        {
            IsMouseOver = true;
            if (IsSelected) return;

            OnMouseOverOutline();
        }

        public void OnMouseExit()
        {
            IsMouseOver = false;

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

        private void OnMouseOverOutline()
        {
            foreach (Enemy enemy in TargetSoliders)
            {
                enemy.OutlineCompo.enabled = true;
                if (enemy.OutlineCompo.isActiveAndEnabled)
                {
                    enemy.OutlineCompo.SetColor(_mouseOverColor);
                }
            } //end foreach
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