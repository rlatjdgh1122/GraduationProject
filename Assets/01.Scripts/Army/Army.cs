using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArmySystem
{
    [System.Serializable]
    public class Army
    {
        public float MoveSpeed;
        public string LegionName; //���° ����
        public bool IsArmyReady = true; //���� ��ü�� ������ �غ� �Ǿ��°�
        public MovefocusMode MovefocusMode = MovefocusMode.Command;
        public ArmyFollowCam FollowCam = null; //���� ������Ʈ
        public ArmyData Info; //����

        public List<Penguin> Soldiers = new(); //���� ��ϵ�
        public General General = null; //�屺

        public EnemyArmy TargetEnemyArmy = null;

        public Ability Ability = null; //�ó��� ����

        public void AddStat(Ability incStat)
        {
            if(Ability != null)
            {
                Ability prevAbility = Ability.DeepCopy();
                RemoveStat(prevAbility);
            }

            //if (Ability.Value == incStat.Value) return;
            //UnityEngine.Debug.Log("��������");
            AddStat(incStat.value, incStat.statType, incStat.statMode);
            Ability = incStat.DeepCopy();
        }

        public void RemoveStat(Ability incStat)
        {
            if (incStat == null) return;

           RemoveStat(incStat.value, incStat.statType, incStat.statMode);
            //RemoveStat(5, StatType.Armor, StatMode.Increase);
        }

        public void AddStat(int value, StatType type, StatMode mode)
        {
            //this.General?.AddStat(value, type, mode);

            foreach (var solider in this.Soldiers)
            {
                solider.AddStat(value, type, mode);
            }
        }

        public void RemoveStat(int value, StatType type, StatMode mode)
        {
            //this.General?.RemoveStat(value, type, mode);

            foreach (var solider in this.Soldiers)
            {
                solider.RemoveStat(value, type, mode);
            }
        }



        public Enemy FindNearestEnemy(Penguin penguin)
        {
            if (TargetEnemyArmy == null || TargetEnemyArmy.IsNull) return null;

            Enemy closestEnemy = null;
            Enemy closestUntargetedEnemy = null;
            double closestDistance = double.MaxValue;
            double closestUntargetedDistance = double.MaxValue;

            foreach (Enemy enemy in TargetEnemyArmy.Soldiers)
            {
                double distance = Vector3.Distance(penguin.transform.position, enemy.transform.position);

                if (distance < closestDistance)
                {
                    closestEnemy = enemy;
                    closestDistance = distance;
                }

                if (distance < closestUntargetedDistance && !IsEnemyTargetedByMyArmy(enemy))
                {
                    closestUntargetedEnemy = enemy;
                    closestUntargetedDistance = distance;
                }
            }

            return closestUntargetedEnemy ?? closestEnemy;
        }

        private bool IsEnemyTargetedByMyArmy(Enemy enemy)
        {
            foreach (Penguin soldier in Soldiers)
            {
                if (soldier.CurrentTarget == enemy)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckEmpty()
        {
            if (Soldiers.Count <= 0 && !General) return true;

            return false;
        }

    }
}