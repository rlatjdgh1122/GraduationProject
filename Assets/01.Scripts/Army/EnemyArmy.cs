
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

            //처음 군단 설정할 때도 타겟 정해줌
            _targetSoldiers = soldiers.ToList();
            _temporaryTargetSoldiers = soldiers.ToList();

            //군단 설정
            Soldiers.ForEach(enemy => enemy.JoinEnemyArmy(this));
        }

        private Enemy _singleTarget = null;

        //임시 타겟, 마우스가 호버될 때 이 리스트에 담김
        private List<Enemy> _temporaryTargetSoldiers = new();

        //실제로 타겟을 지정할 땐 이 리스트를 사용하여 지정
        private List<Enemy> _targetSoldiers = new();

        //실제 군단에 포함되어 있는 모든 펭귄들을 담고 있음
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

            //단일 타겟이었는데 죽었다면 타겟 해제
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
        /// A를 누를 때마다 실행됨
        /// </summary>
        public void OnUpdatedSingleTargetMode(bool isSingleTargetMode)
        {
            _isSingleMode = isSingleTargetMode; 
            if (!IsMouseOver) return;

            //아웃라인을 다 지운 후 임시 타겟 지정
            DeSelectedOutline();
            SetTemporaryTarget();

            //군단을 선택한 상태에서 단일 타겟 모드를 해제했다면 그리지않음
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

                //싱글타겟모드지만 타겟이 지정되지 않았다면 아무행동도 하지않음
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