
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace ArmySystem
{
    [System.Serializable]
    public class EnemyArmy
    {
        public EnemyArmy(List<Enemy> soldiers)
        {
            Soldiers = soldiers;

            //ó�� ���� ������ ���� Ÿ�� ������
            _targetSoldiers = soldiers.ToList();
            _temporaryTargetSoldiers = soldiers.ToList();

            //���� ����
            Soldiers.ForEach(enemy => enemy.JoinEnemyArmy(this));
        }

        private Enemy _singleTarget = null;

        //�ӽ� Ÿ��, ���콺�� ȣ���� �� �� ����Ʈ�� ���
        private List<Enemy> _temporaryTargetSoldiers = new();

        //������ Ÿ���� ������ �� �� ����Ʈ�� ����Ͽ� ����
        private List<Enemy> _targetSoldiers = new();

        //���� ���ܿ� ���ԵǾ� �ִ� ��� ��ϵ��� ��� ����
        public List<Enemy> Soldiers = new();
        public IReadOnlyList<Enemy> TargetSoldiers => _targetSoldiers;

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
            _targetSoldiers.Remove(enemy);
            _temporaryTargetSoldiers.Remove(enemy);

            //���� Ÿ���̾��µ� �׾��ٸ� Ÿ�� ����
            if (_isSingleSelected)
            {
                _singleTarget = null;              
                _isArmySelected = false;
                _isSingleSelected = false;

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

        /// <summary>
        /// A�� ���� ������ �����
        /// </summary>
        public void OnUpdatedSingleTargetMode(bool isSingleTargetMode)
        {
            _isSingleMode = isSingleTargetMode; 
            if (!IsMouseOver) return;

            //�ƿ������� �� ���� �� �ӽ� Ÿ�� ����
            DeSelectedOutline();
            SetTemporaryTarget();

            //������ ������ ���¿��� ���� Ÿ�� ��带 �����ߴٸ� �׸�������
            if (_isSeleted && !_isSingleMode) return;
            OnMouseOverOutline();
        }

        private void SetTemporaryTarget()
        {
            _temporaryTargetSoldiers.Clear();

            if (!_isSingleMode)
                _temporaryTargetSoldiers = Soldiers.ToList();

            else
            {
                if (_singleTarget != null)
                    _temporaryTargetSoldiers.Add(_singleTarget);

                //�̱�Ÿ�ٸ������ Ÿ���� �������� �ʾҴٸ� �ƹ��ൿ�� ��������
            }

        }

        private void SetTarget()
        {
            _targetSoldiers.Clear();

            _targetSoldiers = _temporaryTargetSoldiers.ToList();
        }

        #region MouseEvent

        public void OnMouseEnter()
        {
            IsMouseOver = true;
            if (_isSeleted) return;

            DeSelectedOutline();
            SetTemporaryTarget();
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

            SetTarget();
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
            foreach (Enemy enemy in _temporaryTargetSoldiers)
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
            foreach (Enemy enemy in _targetSoldiers)
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
            foreach (Enemy enemy in _targetSoldiers)
            {
                if (enemy.OutlineCompo != null && enemy.OutlineCompo.isActiveAndEnabled)
                {
                    enemy.OutlineCompo.enabled = false;
                }
            }//end foreach
        }

    }


}