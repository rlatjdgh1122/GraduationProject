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
        public bool IsArmyReady = true; //���� ��ü�� ������ �غ� �Ǿ��°�
        public bool IsHealing = false;
        public MovefocusMode MovefocusMode = MovefocusMode.Command;

        public List<Penguin> Soldiers = new(); //���� ��ϵ� ((�屺 ������))
        public List<Penguin> AlivePenguins = new(); //����ִ� ��ϵ� (�屺 ����)
        public List<Penguin> DeadPenguins = new(); //���� ��ϵ� (�屺 ����)

        public General General = null;
        public bool IsGeneral => General != null;

        public EnemyArmy TargetEnemyArmy = null;
        public Ability Ability = null; //�ó��� ����

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

        public string LegionName //���° ����
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
                Debug.Log($"�ش� ����� �̹� �����մϴ�. {penguin.GetInstanceID()}");

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

            if (CheckSynergy(general)) //�ó����� Ȱ��ȭ �Ǿ��� ���
            {
                SignalHub.OnSynergyEnableEvent?.Invoke(SynergyType);
            }
            else //Ÿ���� �ȸ��� ��� �ó��� ��Ȱ��ȭ
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
                Debug.Log("�ش� ����� �̹� �׾��ֽ��ϴ�.");
            }
        }

        public void RemoveGeneral()
        {
            if (!DeadPenguins.Contains(General))
            {
                AlivePenguins.Remove(General);
                DeadPenguins.Add(General);

                Info.RemovePenguinCount();

                //�ó��� ��Ȱ��ȭ
                SignalHub.OnSynergyDisableEvent?.Invoke(SynergyType);

                General = null;
                SkillController = null;
                UltimateController = null;
            }
            else
            {
                Debug.Log("�屺�� �̹� �׾��ֽ��ϴ�.");
            }
        }

        //�����ý��ۿ��� �츱 �� ���
        public void ResurrectPenguin(Penguin penguin)
        {
            if (!AlivePenguins.Contains(penguin))
            {
                if (penguin is General) //�屺�̶��
                {
                    if (_myGeneral == null) return;

                    General = _myGeneral;
                    SkillController = General.Skill.SkillController;
                    UltimateController = General.Ultimate.SkillController;

                    //�ó��� �ٽ� Ȱ��ȭ����
                    SignalHub.OnSynergyEnableEvent?.Invoke(SynergyType);

                } //end if

                DeadPenguins.Remove(penguin);
                AlivePenguins.Add(penguin);

                Info.AddPenguinCount();

            } //end if
            else
            {
                Debug.Log("�ش� ����� �̹� ����ֽ��ϴ�.");
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
            //UnityEngine.Debug.Log("��������");
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

                // �� ���� �� ���� �Ʊ��� ���� Ÿ���õǰ� �ִ��� ���
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

                // ��� ���� ��ȸ�ϸ� Ÿ���� ī��Ʈ�� ���� �Ÿ��� ����� ���� ����
                foreach (Enemy enemy in TargetEnemyArmy.TargetSoliders)
                {
                    if (enemy == null) continue;

                    double distance = Vector3.Distance(penguin.transform.position, enemy.transform.position);
                    int targetCount = enemyTargetCounts.ContainsKey(enemy) ? enemyTargetCounts[enemy] : 0;

                    // ���� Ÿ���õ� �Ʊ��� ���� ����, �Ÿ��� ���� ����� ���� ����
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