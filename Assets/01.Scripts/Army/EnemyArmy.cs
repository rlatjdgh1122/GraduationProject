
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
			_soldiers = soldiers.ToList();

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

		private Color _mouseOverColor = Color.white;
		private Color _selectColor = Color.white;
		private Color _singleTargetColor = Color.white;

		private bool IsNull => _soldiers == null;

		private bool _isSelected = false;
		private bool _isSingleTargetSelected = false;

		public void Setting(Color overColor, Color selectColor, Color singleTargetColor)
		{
			_mouseOverColor = overColor;
			_selectColor = selectColor;
			_singleTargetColor = singleTargetColor;
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

		public void SetSingleTargetMode(bool isSingleTargetMode)
		{
			//Ÿ���� �����ϱ��� �ƿ������� �� ���� ��
			DeSelectedOutline();

			TargetSoliders.Clear();
			_isSingleTargetSelected = isSingleTargetMode;

			if (isSingleTargetMode)
			{
				//���� Ÿ������
				TargetSoliders.Add(_singleTarget);

			} //end if
			else
			{
				//���� Ÿ������, �������ϱ�
				TargetSoliders = _soldiers.ToList();

			} //end else

			//Ÿ���� �����Ǿ��ٸ� �ٽ� �ƿ������� ����
			OnSelectedOutline();
		}

		#region MouseEvent

		public void OnMouseEnter()
		{
			if (!_isSingleTargetSelected && _isSelected) return;

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
			if (_isSelected)
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
			_isSelected = true;

			OnSelectedOutline();
		}

		public void DeSelected()
		{
			_isSelected = false;

			DeSelectedOutline();
		}

		private void OnSelectedOutline()
		{
			Color color = EnemyArmyManager.Instance.IsSingleTargetMode == true ? _singleTargetColor : _selectColor;

			foreach (Enemy enemy in TargetSoliders)
			{
				enemy.OutlineCompo.enabled = true;
				if (enemy.OutlineCompo != null && enemy.OutlineCompo.isActiveAndEnabled)
				{
					enemy.OutlineCompo.SetColor(color);
				}

			} //end foreach
		}

		private void DeSelectedOutline()
		{
			foreach (Enemy enemy in TargetSoliders)
			{
				if (enemy.OutlineCompo != null && enemy.OutlineCompo.isActiveAndEnabled)
				{
					enemy.OutlineCompo.enabled = false;
				}

			} //end foreach
		}

	}


}