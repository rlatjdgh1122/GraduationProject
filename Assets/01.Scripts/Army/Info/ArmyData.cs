namespace ArmySystem
{
    [System.Serializable]
    public struct ArmyData //UI부분, 기획이 더 필요
    {
        //인원수
        public int totalCount;
        public int basicCount;
        public int archerCount;
        public int shieldCount;

        //스탯이 몇배정도 증가하였는가
        public float basicTimes;
        public float archerTimes;
        public float shieldTimes;
    }
}