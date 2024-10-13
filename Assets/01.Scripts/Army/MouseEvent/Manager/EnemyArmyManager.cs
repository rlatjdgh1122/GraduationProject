using ArmySystem;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class EnemyArmyManager : Singleton<EnemyArmyManager>
{
	public List<EnemyArmy> enemyArmies = new();
	public Color MouseOverColor = Color.white;
	public Color SelectedColor = Color.white;
	public Color SingleTargetSelectedColor = Color.white;
	public KeyCode SingleTargetKey = KeyCode.None;

	private EnemyArmy CurrnetEnemyArmy = null;

	private bool _isSingleTargetMode = false;

	public bool IsSingleTargetMode
	{
		get => _isSingleTargetMode;
		private set
		{
			_isSingleTargetMode = value;

			if (CurrnetEnemyArmy == null) return;

			//타겟이 이미 지정되어 있는 상태라면 수동으로 호출해줌
			CurrnetEnemyArmy.SetSingleTargetMode(_isSingleTargetMode);
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(SingleTargetKey))
		{
			_isSingleTargetMode = true;
		}

		else if (Input.GetKeyUp(SingleTargetKey))
		{
			_isSingleTargetMode = false;
		}
	}

	public EnemyArmy CreateArmy(List<Enemy> enemies)
	{
		var army = new EnemyArmy(enemies);
		army.Setting(MouseOverColor, SelectedColor, SingleTargetSelectedColor);

		enemyArmies.Add(army);

		return army;
	}

	public void DeleteArmy(EnemyArmy army)
	{
		enemyArmies.Remove(army);
	}

	public void OnSelected(EnemyArmy army) //군단 변경될때마다 여기에 타겟을 넣어줌
	{
		if (army != null)
		{
			CurrnetEnemyArmy = army;

			//선택된 군단 말곤 다 선택해제해줌
			enemyArmies.ObjExcept
				(
						army,
						me => me.OnSelected(),
						other => other.DeSelected()
				);// end ObjExcept
		}//end if
		else //타겟이 없을경우
		{
			foreach (EnemyArmy item in enemyArmies)
			{
				item.DeSelected();
			}
		}

	}

	public void DeSelected()
	{
		if (CurrnetEnemyArmy == null) return;

		CurrnetEnemyArmy.DeSelected();

		CurrnetEnemyArmy = null;
	}
}
