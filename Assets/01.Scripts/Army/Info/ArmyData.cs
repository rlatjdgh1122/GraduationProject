namespace ArmySystem
{
    [System.Serializable]
    public struct ArmyData //UI�κ�, ��ȹ�� �� �ʿ�
    {
        //�ο���
        public int totalCount;
        public int basicCount;
        public int archerCount;
        public int shieldCount;

        //������ ������� �����Ͽ��°�
        public float basicTimes;
        public float archerTimes;
        public float shieldTimes;
    }
}