using SynergySystem;
using System;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;



namespace ArmySystem
{


    [System.Serializable]
    public class Army
    {
        public SynergyType SynergyType;

        public int LegionIdx = 0;
        public bool IsArmyReady = true; //군단 전체가 움직일 준비가 되었는가
        public MovefocusMode MovefocusMode = MovefocusMode.Command;

        public List<Penguin> Soldiers = new(); //군인 펭귄들
        public General General = null; //장군

        public EnemyArmy TargetEnemyArmy = null;
        public Ability Ability = null; //시너지 스탯

        public SkillController SkillController = null;
        public SkillController UltimateController = null;
        public ArmyUIInfo Info = new();

        private float _moveSpeed = 4f;
        private string _legionName = string.Empty;

        public bool IsSynergy
        {
            get
            {
                if (!General) return false;
                return General.SynergyType.Equals(SynergyType);
            }
        }


        #region Property

        public float MoveSpeed
        {
            get => _moveSpeed;

            set
            {
                _moveSpeed = value;
                OnMoveSpeedUpdated?.Invoke(_moveSpeed);
            }
        }

        public string LegionName //몇번째 군단
        {
            get => _legionName;

            set
            {
                OnLegionNameChanged?.Invoke(_legionName, value);
                _legionName = value;
            }
        }

        #endregion

        #region EventHandler

        public OnValueUpdated<float> OnMoveSpeedUpdated = null;
        public OnValueChanged<string> OnLegionNameChanged = null;

        #endregion

        public void AddSolider(Penguin penguin)
        {
            Soldiers.Add(penguin);
            Info.AddPenguinCount();
        }

        public void AddGeneral(General general)
        {
            General = general;

            SkillController = general.Skill.SkillController;
            UltimateController = general.Ultimate.SkillController;

            Info.AddPenguinCount();
        }

        public void RemoveSolider(Penguin penguin)
        {
            Soldiers.Remove(penguin);

            Info.RemovePenguinCount();
        }

        public void RemoveGeneral()
        {
            General = null;
            SkillController = null;

            Info.RemovePenguinCount();
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
            //UnityEngine.Debug.Log("스탯증가");
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
            if (TargetEnemyArmy == null) return null;
            if (TargetEnemyArmy.Soldiers == null) return null;
            if (TargetEnemyArmy.Soldiers.Count <= 0) return null;

            Enemy closestEnemy = null;
            Enemy closestUntargetedEnemy = null;
            double closestDistance = double.MaxValue;
            double closestUntargetedDistance = double.MaxValue;

            try
            {
                // Soldiers 리스트를 순회하며 가장 가까운 적을 찾음
                foreach (Enemy enemy in TargetEnemyArmy.Soldiers)
                {
                    // enemy가 null인 경우를 확인
                    if (enemy == null) continue;

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

                // 가장 가까운 타겟이 지정되지 않은 적 또는 가장 가까운 적 반환
                return closestUntargetedEnemy ?? closestEnemy;
            }
            catch (NullReferenceException ex)
            {
                return null;
            }

        }


        private bool IsEnemyTargetedByMyArmy(Enemy enemy)
        {
            if (TargetEnemyArmy.Soldiers == null) return false;

            foreach (Penguin soldier in Soldiers)
            {
                // enemy가 null인 경우를 확인
                if (enemy == null) continue;

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

        public IValueChanger<ArmyUIInfo> GetInfo()
        {
            return Info;
        }
    }
}