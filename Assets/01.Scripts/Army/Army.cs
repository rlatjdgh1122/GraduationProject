using SynergySystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ArmySystem
{
    [System.Serializable]
    public class Army
    {
        public SynergySystem.SynergyType SynergyType;

        private float _moveSpeed = 4f;
        private string _legionName = string.Empty;

        public float MoveSpeed
        {
            get => _moveSpeed;

            set
            {
                _moveSpeed = value;
                OnMoveSpeedUpdated?.Invoke(_moveSpeed);
            }
        }


        public string LegionName //���° ����
        {
            get => _legionName;

            set
            {
                OnLegionNameChanged?.Invoke(_legionName, value);
                _legionName = value;
            }
        }

        public OnValueChanged<string> OnLegionNameChanged = null;
        public OnValueUpdated<float> OnMoveSpeedUpdated = null;

        public bool IsArmyReady = true; //���� ��ü�� ������ �غ� �Ǿ��°�
        public MovefocusMode MovefocusMode = MovefocusMode.Command;
        public ArmyFollowCam FollowCam = null; //���� ķ
        public ArmyData Info; //����

        public List<Penguin> Soldiers = new(); //���� ��ϵ�
        public General General = null; //�屺

        public EnemyArmy TargetEnemyArmy = null;

        public Ability Ability = null; //�ó��� ����

        public SkillTransition SkillTransition = null;

        public void SetGeneral(General general)
        {
            General = general;
            SkillTransition = general.Skill.SkillTransition;
        }

        #region Stat

        public void AddStat(Ability incStat)
        {
            if (Ability != null)
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

        #endregion

        #region Find Enemy
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

        #endregion

        public bool CheckEmpty()
        {
            if (Soldiers.Count <= 0 && !General) return true;

            return false;
        }

    }
}