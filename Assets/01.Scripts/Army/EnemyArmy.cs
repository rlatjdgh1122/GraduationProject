
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

            //ó�� ���� ������ ���� Ÿ�� ������
            TargetSoliders = soldiers.ToList();

            //���� ����
            Soldiers.ForEach(enemy => enemy.JoinEnemyArmy(this));

        }

        //�갡 null�̶�� ���� ������ �ȵ� �Ͱ� ���� 
        private Enemy _singleTarget = null;

        //���� ���ܿ� ���ԵǾ� �ִ� ��� ��ϵ��� ��� ����
        public List<Enemy> Soldiers = new();

        //������ Ÿ���� ������ �� �� ����Ʈ�� ����Ͽ� ����
        public List<Enemy> TargetSoliders = new();

        public int SoliderCount => Soldiers.Count;
        public bool IsMouseOver = false;

        private bool _isSingleMode = false;
        private bool _isArmySelected = false;
        private bool _isSingleSelected = false;

        private bool _isSeleted => _isArmySelected || _isSingleSelected;

        private Color _mouseOverColor = Color.white;
        private Color _selectColor = Color.white;

        private bool IsNull => Soldiers == null;

        public void OnChangedOutlineColorHandler(Color overColor, Color selectColor)
        {
            _mouseOverColor = overColor;
            _selectColor = selectColor;
        }

        public void RemoveEnemy(Enemy enemy)
        {
            Soldiers.Remove(enemy);
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

        /// <summary>
        /// A�� ���� ������ �����
        /// </summary>
        public void OnUpdatedSingleTargetMode(bool isSingleTargetMode)
        {
            _isSingleMode = isSingleTargetMode;

            //Ÿ���� �����ϱ��� �ƿ������� �� ���� ��
            DeSelectedOutline();

            if (_isSeleted) //Ÿ���� ������ ��Ȳ�̶��
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

        private void SingleTarget()
        {
            TargetSoliders.Clear();

            //���� Ÿ������
            if (_singleTarget != null)
                TargetSoliders.Add(_singleTarget);
        }

        private void ArmyTarget()
        {
            TargetSoliders.Clear();

            //���� Ÿ������, �������ϱ�
            TargetSoliders = Soldiers.ToList();
        }

        #region MouseEvent

        public void OnMouseEnter()
        {
            IsMouseOver = true;
            if (_isSeleted) return;

            OnMouseOverOutline();
        }

        public void OnMouseExit()
        {
            IsMouseOver = false;
            if (_isSeleted)
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
            if (_isSingleMode)
            {
                _isArmySelected = false;
                _isSingleSelected = true;

            } //end if

            else
            {
                _isArmySelected = true;
                _isSingleSelected = false;

            } //end if

            OnSelectedOutline();
        }

        public void DeSelected()
        {
            _isArmySelected = false;
            _isSingleSelected = false;

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