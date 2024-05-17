using System.Collections.Generic;

namespace ArmySystem
{
    [System.Serializable]
    public class Army
    {
        public float MoveSpeed;
        public string LegionName; //몇번째 군단
        //public bool IsCanReadyAttackInCurArmySoldiersList = true; //군단 전체가 움직일 준비가 되었는가
        public MovefocusMode MoveFocusMode; //모드 바꾸기

        public ArmyFollowCam FollowCam = null; //군단 오브젝트
        public ArmyData Info; //정보

        public List<Penguin> Soldiers = new(); //군인 펭귄들
        public General General = null; //장군

        public Ability Ability = null; //시너지 스탯

        public void AddStat(Ability incStat)
        {
            RemoveStat();
            this.Ability = incStat;
            //if (Ability.Value == incStat.Value) return;
            //UnityEngine.Debug.Log("스탯증가");
            AddStat(incStat.value, incStat.statType, incStat.statMode);
        }

        public void RemoveStat()
        {
            if (Ability == null) return;
            //UnityEngine.Debug.Log("스탯빼주기");

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