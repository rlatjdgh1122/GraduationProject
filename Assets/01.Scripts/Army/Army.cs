using System.Collections.Generic;

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
            RemoveStat();
            this.Ability = incStat;
            //if (Ability.Value == incStat.Value) return;
            //UnityEngine.Debug.Log("��������");
            AddStat(incStat.value, incStat.statType, incStat.statMode);
        }

        public void RemoveStat()
        {
            if (Ability == null) return;
            //UnityEngine.Debug.Log("���Ȼ��ֱ�");

            RemoveStat(Ability.value, Ability.statType, Ability.statMode);

            Ability = null;
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