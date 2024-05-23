using System.Collections.Generic;
using UnityEngine;

namespace ArmySystem
{
    [System.Serializable]
    public class Army
    {
        public float MoveSpeed;
        public string LegionName; //���° ����
        //public bool IsCanReadyAttackInCurArmySoldiersList = true; //���� ��ü�� ������ �غ� �Ǿ��°�
        public MovefocusMode MoveFocusMode; //��� �ٲٱ�
        public ArmyFollowCam FollowCam = null; //���� ������Ʈ
        public ArmyData Info; //����

        public List<Penguin> Soldiers = new(); //���� ��ϵ�
        public General General = null; //�屺

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

        public bool CheckEmpty()
        {
            if (Soldiers.Count <= 0 && !General) return true;

            return false;
        }

    }
}