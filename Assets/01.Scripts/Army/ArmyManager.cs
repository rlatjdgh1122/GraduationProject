using ArmySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArmyManager : Singleton<ArmyManager>
{
    [SerializeField] private List<Army> armies;

    public List<Army> Armies { get { return armies; } }
    private Dictionary<KeyCode, Action> keyDictionary = new();

    [SerializeField] private MovefocusMode curFocusMode = MovefocusMode.Battle;
    [SerializeField] private Color battleModeOutline = Color.white;
    [SerializeField] private Color commandModeOutline = Color.white;

    public MovefocusMode CurFocusMode => curFocusMode;

    private Color GetOutlineColor => curFocusMode == MovefocusMode.Command ? commandModeOutline : battleModeOutline;

    private int curArmyIdx = -1;

    public int CurLegion
    {
        get
        {
            int curLegion = curArmyIdx + 1;

            if (curLegion <= 0) return 1;
            return curLegion;
        }
    }

    public int ArmiesCount => armies.Count;

    public override void Awake()
    {
        KeySetting();

    }
    private void Start()
    {
        if (armies.Count > 0)
            armies.Clear();

        CreateArmy();
        SignalHub.OnBattleModeChanged?.Invoke(curFocusMode);
    }

    private void KeySetting()
    {
        keyDictionary = new Dictionary<KeyCode, Action>()
        {
             {KeyCode.Alpha1, ()=> ChangeArmy(1) },
             {KeyCode.Alpha2, ()=> ChangeArmy(2) },
             {KeyCode.Alpha3, ()=> ChangeArmy(3) },
             {KeyCode.Alpha4, ()=> ChangeArmy(4) },
             {KeyCode.Alpha5, ()=> ChangeArmy(5) },
             {KeyCode.Alpha6, ()=> ChangeArmy(6) },
             {KeyCode.Alpha7, ()=> ChangeArmy(7) },
             {KeyCode.Alpha8, ()=> ChangeArmy(8) },
             {KeyCode.Alpha9, ()=> ChangeArmy(9) },
             {KeyCode.A,      ()=> OnBattleModeChanged() },
        };
    }

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (var dic in keyDictionary)
            {
                if (Input.GetKeyDown(dic.Key))
                {
                    dic.Value();
                }
            }
        }
    }

    private void OnBattleModeChanged()
    {
        if (!WaveManager.Instance.IsBattlePhase) return;

        curFocusMode = curFocusMode == MovefocusMode.Command ? MovefocusMode.Battle : MovefocusMode.Command;
        SignalHub.OnBattleModeChanged?.Invoke(curFocusMode);

        var curArmy = GetCurArmy();
        curArmy.MoveFocusMode = curFocusMode;

        curArmy.Soldiers.ForEach(s =>
        {
            s.OutlineCompo?.SetColor(GetOutlineColor);

            if (curFocusMode == MovefocusMode.Battle)
                s.FindNearestEnemy();
        });

        if (curArmy.General)
        {
            curArmy.General.OutlineCompo?.SetColor(GetOutlineColor);

            if (curFocusMode == MovefocusMode.Battle)
                curArmy.General.FindNearestEnemy();
        }

    }

    public void ChangedCurrentArmy()
    {
        ChangeArmy(CurLegion);
    }


    /// <summary>
    /// 군단 변경
    /// </summary>
    /// <param name="legion"> 몇번째 군단</param>
    private void ChangeArmy(int legion)
    {

        //전투 라운드가 아니면 실행안해줌
        if (!WaveManager.Instance.IsBattlePhase) return;

        //아직 생성되지 않은 군단에 접근하면 리턴해줌
        if (armies.Count < legion) return;

        SignalHub.OnModifyCurArmy?.Invoke();

        int Idx = legion - 1;
        var curArmy = armies[Idx];

        var General = curArmy.General;

        //중복 선택된 군단도 아웃라인 보이게
        curArmy.Soldiers.ForEach(s =>
        {
            s.OutlineCompo.enabled = true;
            s.OutlineCompo.SetColor(GetOutlineColor);

            s.HealthCompo.IsAlwaysShowUI = true;
            s.HealthCompo?.OnUIUpdate?.Invoke(s.HealthCompo.currentHealth, s.HealthCompo.maxHealth);
        });

        if (General)
        {
            var GeneralHealtCompo = General.HealthCompo;

            General.OutlineCompo.enabled = true;
            General.OutlineCompo.SetColor(GetOutlineColor);

            GeneralHealtCompo.IsAlwaysShowUI = true;
            GeneralHealtCompo?.OnUIUpdate?.Invoke(GeneralHealtCompo.currentHealth, GeneralHealtCompo.maxHealth);
        }

        //군단 체인지 하는건 한 번만 실행해도 되니깐
        //이전과 같은 군단을 선택했다면 리턴
        if (curArmyIdx == Idx) return;

        if (curArmy.CheckEmpty())
        {
            UIManager.Instance.ShowWarningUI($"{curArmy.LegionName}에는 펭귄이 존재하지 않습니다.");
            return;
        }
        else
            UIManager.Instance.ShowWarningUI($"{curArmy.LegionName} 선택");

        var prevIdx = curArmyIdx < 0 ? 0 : curArmyIdx;
        curArmyIdx = Idx;

        SignalHub.OnArmyChanged.Invoke(armies[prevIdx], armies[Idx]);

        armies.IdxExcept
            (
            Idx,

            p => //선택된 군단은 아웃라인을 켜줌
            {
                //curArmy set battleMode
                p.MoveFocusMode = curFocusMode;

                var color = curFocusMode == MovefocusMode.Command ? commandModeOutline : battleModeOutline;


                p.Soldiers.ForEach(s =>
                {
                    s.OutlineCompo?.SetColor(color);

                    if (CurFocusMode == MovefocusMode.Battle)
                    {
                        s.FindNearestEnemy();
                    }
                });
            },

           p => //선택되지 않은 나머지 군단은 아웃라인을 켜줌
           {
               p.Soldiers.ForEach(s =>
               {
                   s.OutlineCompo.enabled = false;
                   s.HealthCompo.IsAlwaysShowUI = false;
                   s.HealthCompo.OffUIUpdate?.Invoke();
               });

               if (p.General)
               {
                   p.General.OutlineCompo.enabled = false;
                   p.General.HealthCompo.IsAlwaysShowUI = false;
                   p.General.HealthCompo.OffUIUpdate?.Invoke();
               }
           }); //end IdxExcept

    } //end method

    /// <summary>
    /// 현재 선택된 Army를 리던
    /// </summary>
    /// <returns> Army를 리던</returns>
    public Army GetCurArmy()
    {
        var idx = curArmyIdx < 0 ? 0 : curArmyIdx;
        return armies[idx];
    }

    public Army GetArmy(int legion)
    {
        return armies[legion - 1];
    }

    #region 스탯 부분
    /// <summary>
    /// 현재 선택된 군단의 스탯을 상승 및 감소 
    /// </summary>
    /// <param name="value"> 값(%)</param>
    /// <param name="type"> 값을 변경할 스탯타입 </param>
    /// <param name="mode"> 상승 또는 감소</param>
    public void AddStatCurAmry(int value, StatType type, StatMode mode)
    {
        armies[curArmyIdx].AddStat(value, type, mode);
    }

    /// <summary>
    /// 현재 선택된 군단의 스탯을 삭제
    /// </summary>
    /// <param name="value"> 삭제할 값(단, 똑같은 값이 존재해야함)</param>
    /// <param name="type"> 값을 삭제할 스탯타입 </param>
    /// <param name="mode"> 상승 또는 감소</param>
    public void RemoveStatCurAmry(int value, StatType type, StatMode mode)
    {
        armies[curArmyIdx].RemoveStat(value, type, mode);
    }

    /// <summary>
    /// 군단의 스탯을 상승 및 감소
    /// </summary>
    /// <param name="legion"> 몇번째 군단 </param>
    /// <param name="value"> 값(%)</param>
    /// <param name="type"> 값을 변경할 스탯타입</param>
    /// <param name="mode"> 상승 또는 감소</param>
    public void RemoveStat(int legion, int value, StatType type, StatMode mode)
    {
        armies[legion - 1].RemoveStat(value, type, mode);
    }
    /// <summary>
    /// 군단의 스탯을 삭제
    /// </summary>
    /// <param name="legion"> 몇번째 군단 </param>
    /// <param name="value"> 값(%)</param>
    /// <param name="type"> 값을 변경할 스탯타입</param>
    /// <param name="mode"> 상승 또는 감소</param>
    public void AddStat(int legion, int value, StatType type, StatMode mode)
    {
        armies[legion - 1].AddStat(value, type, mode);
    }

    #endregion

    #region 군단 영입 부분

    /// <summary>
    /// 장군을 제외한 펭귄을 군단에 넣는 함수
    /// </summary>  
    /// <param name="legion"> 몇번째 군단</param>
    /// <param name="obj"> Penguin 타입만 가능</param>
    public void JoinArmyToSoldier(string legion, Penguin obj) //들어가고 싶은 군단, 군인펭귄
    {
        if (armies.Find(p => p.LegionName == legion) == null)
        {
            Debug.Log("그런 군단 이름은 없습니다.");
            return;
        }
        int idx = LegionInventoryManager.Instance.GetLegionIdxByLegionName(legion);
        var Army = armies[idx];

        obj.SetOwner(Army);
        Army.Soldiers.Add(obj);

        if (Army.Ability != null)
        {
            //들어왓는데 시너지가 잇다면 스탯추가
            obj.AddStat(Army.Ability);
        }

    }

    /// <summary>
    /// 장군펭귄을 군단에 넣는 함수
    /// </summary>
    /// <param name="legion"> 몇번째 군단</param>
    /// <param name="obj"> Penguin 타입만 가능</param>
    public void JoinArmyToGeneral(string legion, General obj) //들어가고 싶은 군단, 장군펭귄
    {
        if (armies.Find(p => p.LegionName == legion) == null)
        {
            Debug.Log("그런 군단 이름은 없습니다.");
            return;
        }

        var Idx = LegionInventoryManager.Instance.GetLegionIdxByLegionName(legion);
        var Army = armies[Idx];

        if (Army.General != null)
        {
            Debug.Log($"현재 {legion}군단에는 장군이 존재합니다.");
            return;
        }

        obj.SetOwner(Army);
        Army.General = obj;
        var stat = obj.ReturnGenericStat<GeneralStat>();


        stat.GeneralDetailData.synergy.Stat.OnValidate += Army.AddStat;
        //시너지 스탯 연결

        //인보크
        stat.GeneralDetailData.synergy.InvokeOnValidate();
    }

    #endregion

    #region 펭귄 및 군단 생성 부분

    /// <summary>
    /// 펭귄 지우기
    /// </summary>
    /// <param name="legion"> 몇번째 군단 *owner.Legion 입력*</param>
    /// <param name="penguin"> Penguin 타입만 가능 *this 입력*</param>

    public void RemovePenguin(string legion, Penguin penguin)
    {
       //군단지우는거 여기서 하는데 굳이 이렇게 할 필요없이
       //그냥 자기 군단에서 remove함수 만들어서 하면 될듯
       //대신 군단에서 스탯빠지는건 해주고 (장군, 펭귄따로)



        //증가된 군단 스탯 지우기
        int idx = LegionInventoryManager.Instance.GetLegionIdxByLegionName(legion);
        var Army = armies[idx];
        //var Abilities = Army.Abilities;

        penguin.owner = (null);

        //장군이라면
        if (penguin is General)
        {
            var stat = penguin.ReturnGenericStat<GeneralStat>();
            stat.GeneralDetailData.synergy.Stat.OnValidate -= Army.AddStat;
            Army.RemoveStat(Army.Ability);

            Army.Ability = null;

            armies[idx].General = null;
        }
        else if (penguin is Penguin)
        {
            //군단 리스트에서 제외
            Army.Soldiers.Remove(penguin);

            if (Army.Ability != null)
                penguin.RemoveStat(Army.Ability);
        }
    }

    /// <summary>
    /// 새로운 군단 생성
    /// </summary>
    public void CreateArmy()
    {
        Army newArmy = new Army();

        newArmy.MoveSpeed = 4f;
        newArmy.LegionName = $"{ArmiesCount + 1}군단";
        newArmy.IsArmyReady = false;

        armies.Add(newArmy);
        GameObject followCam = new GameObject($"{newArmy.LegionName}Legion_FollowCam");
        ArmyFollowCam armyFollowCam = new ArmyFollowCam();

        //위치 초기화
        followCam.transform.position = new Vector3(4.18f, 20f, 1.8f);

        armyFollowCam.Obj = followCam;
        newArmy.FollowCam = armyFollowCam;

    }

    #endregion

    public bool CheckEmpty()
    {
        if (armies.Count <= 0) return true;

        bool result = armies.All(x => x.CheckEmpty());
        return result;
    }
}
