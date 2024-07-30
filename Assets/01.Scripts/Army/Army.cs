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
        public MovefocusMode MovefocusMode = MovefocusMode.Command;

        public List<Penguin> Soldiers = new(); //���� ��ϵ�
        public List<Penguin> AliveSoldiers = new(); //����ִ� ��ϵ�
        public List<Penguin> DeadSoldiers = new(); //���� ��ϵ�

        public General General = null; //�屺�� �� ����Ʈ�� ���Ե��� ���� 
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
            if (Soldiers.Contains(penguin))
            {
                Soldiers.Add(penguin);
                AliveSoldiers.Add(penguin);

                Info.AddPenguinCount();
            }
            else
            {
                Debug.Log("�ش� ����� �̹� �����մϴ�.");
            }
        }

        public void AddGeneral(General general)
        {
            General = general;
            _myGeneral = general;

            SkillController = general.Skill.SkillController;
            UltimateController = general.Ultimate.SkillController;

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

        public void RemoveSolider(Penguin penguin)
        {
            if (AliveSoldiers.Contains(penguin))
            {
                AliveSoldiers.Remove(penguin);
                DeadSoldiers.Add(penguin);

                Info.RemovePenguinCount();
            }
            else
            {
                Debug.Log("�ش� ����� �̹� �׾��ֽ��ϴ�.");
            }
        }

        public void RemoveGeneral()
        {
            //�ó��� ��Ȱ��ȭ
            SignalHub.OnSynergyDisableEvent?.Invoke(SynergyType);

            General = null;
            SkillController = null;

            Info.RemovePenguinCount();
        }

        //�����ý��ۿ��� �츱 �� ���
        public void ResurrectSoldier(Penguin penguin)
        {
            if (DeadSoldiers.Contains(penguin))
            {
                DeadSoldiers.Remove(penguin);
                AliveSoldiers.Add(penguin);

                Info.AddPenguinCount();
            }
            else
            {
                Debug.Log("�ش� ����� �̹� ����ֽ��ϴ�.");
            }
        }

        public void ResurrectGeneral()
        {
            if()
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
            if (TargetEnemyArmy.Soldiers == null) return null;
            if (TargetEnemyArmy.Soldiers.Count <= 0) return null;

            Enemy closestEnemy = null;
            Enemy closestUntargetedEnemy = null;
            double closestDistance = double.MaxValue;
            double closestUntargetedDistance = double.MaxValue;

            try
            {
                // Soldiers ����Ʈ�� ��ȸ�ϸ� ���� ����� ���� ã��
                foreach (Enemy enemy in TargetEnemyArmy.Soldiers)
                {
                    // enemy�� null�� ��츦 Ȯ��
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

                // ���� ����� Ÿ���� �������� ���� �� �Ǵ� ���� ����� �� ��ȯ
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
                // enemy�� null�� ��츦 Ȯ��
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