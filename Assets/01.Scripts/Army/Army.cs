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
        public bool IsHealing = false;
        public MovefocusMode MovefocusMode = MovefocusMode.Command;

        public List<Penguin> Soldiers = new(); //군인 펭귄들 ((장군 미포함))
        public List<Penguin> AlivePenguins = new(); //살아있는 펭귄들 (장군 포함)
        public List<Penguin> DeadPenguins = new(); //죽은 펭귄들 (장군 포함)

        public General General = null;
        public bool IsGeneral => General != null;

        public EnemyArmy TargetEnemyArmy = null;
        public Ability Ability = null; //시너지 스탯

        public SkillController SkillController = null;
        public SkillController UltimateController = null;
        public ArmyUIInfo Info = new();

        private General _myGeneral = null;
        private float _moveSpeed = 4f;
        private string _legionName = string.Empty;

        #region Property

        public bool IsSynergy
        {
            get
            {
                if (!General) return false;
                return General.SynergyType.Equals(SynergyType);
            }
        }

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

        private bool CheckSynergy(General general)
        {
            if (!general) return false;
            return general.SynergyType.Equals(SynergyType);
        }

        public void AddSolider(Penguin penguin)
        {
            if (!Soldiers.Contains(penguin))
            {
                Soldiers.Add(penguin);
                Debug.Log(Soldiers.Count);
                AlivePenguins.Add(penguin);

                Info.AddPenguinCount();
            }
            else
            {
                Debug.Log($"해당 펭귄은 이미 존재합니다. {penguin.GetInstanceID()}");

            }
        }

        public void AddGeneral(General general)
        {
            General = general;
            _myGeneral = general;

            SkillController = general.Skill.SkillController;
            UltimateController = general.Ultimate.SkillController;

            AlivePenguins.Add(general);
            Info.AddPenguinCount();

            if (CheckSynergy(general)) //시너지가 활성화 되었을 경우
            {
                SignalHub.OnSynergyEnableEvent?.Invoke(SynergyType);
            }
            else //타입이 안맞을 경우 시너지 비활성화
            {
                SignalHub.OnSynergyDisableEvent?.Invoke(SynergyType);
            }
        }

        public void RemoveSolider(Penguin solider)
        {
            if (!DeadPenguins.Contains(solider))
            {
                AlivePenguins.Remove(solider);
                DeadPenguins.Add(solider);

                Info.RemovePenguinCount();
            }
            else
            {
                Debug.Log("해당 펭귄은 이미 죽어있습니다.");
            }
        }

        public void RemoveGeneral()
        {
            if (!DeadPenguins.Contains(General))
            {
                AlivePenguins.Remove(General);
                DeadPenguins.Add(General);

                Info.RemovePenguinCount();

                //시너지 비활성화
                SignalHub.OnSynergyDisableEvent?.Invoke(SynergyType);

                General = null;
                SkillController = null;
                UltimateController = null;
            }
            else
            {
                Debug.Log("장군은 이미 죽어있습니다.");
            }
        }

        //힐링시스템에서 살릴 때 사용
        public void ResurrectPenguin(Penguin penguin)
        {
            if (!AlivePenguins.Contains(penguin))
            {
                if (penguin is General) //장군이라면
                {
                    if (_myGeneral == null) return;

                    General = _myGeneral;
                    SkillController = General.Skill.SkillController;
                    UltimateController = General.Ultimate.SkillController;

                    //시너지 다시 활성화해줌
                    SignalHub.OnSynergyEnableEvent?.Invoke(SynergyType);

                } //end if

                DeadPenguins.Remove(penguin);
                AlivePenguins.Add(penguin);

                Info.AddPenguinCount();

            } //end if
            else
            {
                Debug.Log("해당 펭귄은 이미 살아있습니다.");
            }
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
            //Ability = incStat.DeepCopy();
        }

        public void RemoveStat(Ability incStat)
        {
            if (incStat == null) return;

            RemoveStat(incStat.value, incStat.statType, incStat.statMode);
            //RemoveStat(5, StatType.Armor, StatMode.Increase);
        }

        public void AddStat(int value, StatType type, StatMode mode)
        {
            this.General?.AddStat(value, type, mode);

            foreach (var solider in this.Soldiers)
            {
                solider.AddStat(value, type, mode);
            }
        }

        public void RemoveStat(int value, StatType type, StatMode mode)
        {
            this.General?.RemoveStat(value, type, mode);

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
            if (TargetEnemyArmy._soldiers == null) return null;
            if (TargetEnemyArmy._soldiers.Count <= 0) return null;

            Enemy closestEnemy = null;
            int fewestTargets = int.MaxValue;
            double closestDistance = double.MaxValue;

            try
            {
                Dictionary<Enemy, int> enemyTargetCounts = new Dictionary<Enemy, int>();

                // 각 적이 몇 명의 아군에 의해 타겟팅되고 있는지 계산
                foreach (Penguin soldier in Soldiers)
                {
                    if (soldier.CurrentTarget != null)
                    {
                        Enemy target = soldier.CurrentTarget as Enemy;
                        if (enemyTargetCounts.ContainsKey(target))
                        {
                            enemyTargetCounts[target]++;
                        }
                        else
                        {
                            enemyTargetCounts[target] = 1;
                        }
                    }
                }

                // 모든 적을 순회하며 타겟팅 카운트가 적고 거리가 가까운 적을 선택
                foreach (Enemy enemy in TargetEnemyArmy.TargetSoliders)
                {
                    if (enemy == null) continue;

                    double distance = Vector3.Distance(penguin.transform.position, enemy.transform.position);
                    int targetCount = enemyTargetCounts.ContainsKey(enemy) ? enemyTargetCounts[enemy] : 0;

                    // 적이 타겟팅된 아군의 수가 적고, 거리가 가장 가까운 적을 선택
                    if (targetCount < fewestTargets || (targetCount == fewestTargets && distance < closestDistance))
                    {
                        closestEnemy = enemy;
                        fewestTargets = targetCount;
                        closestDistance = distance;
                    }
                }

                return closestEnemy;
            }

            catch (NullReferenceException ex)
            {
                return null;
            }
        }


        #endregion

        public bool CheckEmpty()
        {
            if (AlivePenguins.Count <= 0)
            {
                Debug.LogError($"{AlivePenguins.Count}");
                return true;
            }

            return false;
        }

        public IValueChanger<ArmyUIInfo> GetInfo()
        {
            return Info;
        }
    }
}