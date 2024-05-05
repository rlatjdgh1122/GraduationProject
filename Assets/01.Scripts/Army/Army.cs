using System.Collections.Generic;

namespace ArmySystem
{
    [System.Serializable]
    public class Army
    {
        public string LegionName; //���° ����
        public bool IsCanReadyAttackInCurArmySoldiersList = true; //���� ��ü�� ������ �غ� �Ǿ��°�
        public MovefocusMode MoveFocusMode; //��� �ٲٱ�

        public ArmyFollowCam FollowCam = null; //���� ������Ʈ
        public ArmyData Info; //����

        public List<Penguin> Soldiers = new(); //���� ��ϵ�
        public General General = null; //�屺

        public List<Ability> Abilities = new();


        public void AddStat(List<Ability> abilities)
        {
            foreach (var incStat in abilities)
            {
                AddStat(incStat.value, incStat.statType, incStat.statMode);
            }
        }
        public void RemoveStat(List<Ability> abilities)
        {
            foreach (var incStat in abilities)
            {
                RemoveStat(incStat.value, incStat.statType, incStat.statMode);
            }
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
    }
}