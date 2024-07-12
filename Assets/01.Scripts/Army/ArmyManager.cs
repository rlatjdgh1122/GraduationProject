using ArmySystem;
using SynergySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ArmyManager : Singleton<ArmyManager>
{
    [SerializeField] private List<Army> armies;
    [SerializeField] private SettingArmyPostion _settingArmyPsotion = null;

    public General G;
    public List<Army> Armies { get { return armies; } }

    private int prevArmyIdx = -1;
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

    public Army CurArmy
    {
        get
        {
            if (armies.Count <= 0) return null;
            return armies[curArmyIdx < 0 ? 0 : curArmyIdx];
        }
    }

    private SkillInput _skillInput;

    private Transform SpawnPoint => GameManager.Instance.TentTrm;
    private List<Vector3> _armyPostions = new();

    public override void Awake()
    {
        base.Awake();
        _armyPostions = _settingArmyPsotion.Transforms.Convert(p => p.position);

        _skillInput = GetComponent<SkillInput>();
    }
    private void OnEnable()
    {
        SignalHub.OnBattlePhaseStartEvent += OnBattleStart;
        SignalHub.OnBattlePhaseEndEvent += OnBattleEnd;
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseStartEvent -= OnBattleStart;
        SignalHub.OnBattlePhaseEndEvent -= OnBattleEnd;
    }

    private void OnBattleStart()
    {
        var prevArmy = armies[prevArmyIdx > 0 ? prevArmyIdx : 0];
        var curArmy = armies[curArmyIdx > 0 ? curArmyIdx : 0];

        CoroutineUtil.CallWaitForSeconds(0.01f,

            () =>
            {
                SignalHub.OnArmyChanged.Invoke(prevArmy, curArmy);
                _skillInput.SelectGeneral(curArmy.General);
            });

    }

    private void OnBattleEnd()
    {

    }


    private void Start()
    {
        if (armies.Count > 0)
            armies.Clear();

        //CreateArmy();
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.T))
    //    {
    //        var army = CreateArmy("1군단");
    //        var s = Instantiate(G, new Vector3(3.868185f, 1.267861f, -4.28912f), Quaternion.identity);
    //        s.SetOwner(army);
    //        army.General = s;
    //        _skillInput.SelectGeneral(s);
    //        SignalHub.OnModifyCurArmy();
    //    }
    //}

    public void SetTargetEnemyArmy(EnemyArmy enemyArmy)
    {
        CurArmy.TargetEnemyArmy = enemyArmy;

        if (enemyArmy != null) //타겟을 null로 할 경우를 예외처리
            CurArmy.MovefocusMode = MovefocusMode.Battle;
    }



    /// <summary>
    /// 움직일 경우엔 타겟을 명령모드로 변경
    /// </summary>
    public void SetMoveForcusCommand(RaycastHit hit = default)
    {
        CurArmy.MovefocusMode = MovefocusMode.Command;
    }

    public void ChangedCurrentArmy()
    {
        OnChangedArmy(CurLegion);
    }


    /// <summary>
    /// 군단 변경
    /// </summary>
    /// <param name="legion"> 몇번째 군단</param>
    public void OnChangedArmy(int legion)
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
            s.HealthCompo.IsAlwaysShowUI = true;
            s.HealthCompo?.OnUIUpdate?.Invoke(s.HealthCompo.currentHealth, s.HealthCompo.maxHealth);
        });

        if (General)
        {
            var GeneralHealtCompo = General.HealthCompo;

            General.OutlineCompo.enabled = true;
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

        prevArmyIdx = curArmyIdx < 0 ? 0 : curArmyIdx;
        curArmyIdx = Idx;

        SignalHub.OnArmyChanged.Invoke(armies[prevArmyIdx], armies[curArmyIdx]);

        armies.IdxExcept
            (
            Idx,
           null,
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

        EnemyArmyManager.Instance.OnSelected(curArmy.TargetEnemyArmy);

        _skillInput.SelectGeneral(General); // SkillInput에 선택된 장군을 보냄
    } //end method

    /// <summary>
    /// 현재 선택된 Army를 리던
    /// </summary>
    /// <returns> Army를 리던</returns>

    public Army GetArmy(int legion)
    {
        return armies[legion - 1];
    }

    public void SetArmySynergy(int legionIdx, SynergyType synergyType)
    {
        armies[legionIdx].SynergyType = synergyType;
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
    /// <param name="legionName"> 군단 이름 </param>
    /// <param name="legionIdx"> 몇번째 군단</param>
    /// <param name="obj"> Penguin 타입만 가능</param>
    public void JoinArmyToSoldier(string legionName, int legionIdx, Penguin obj) //들어가고 싶은 군단, 군인펭귄
    {
        if (armies.Find(p => p.LegionName == legionName) == null)
        {
            Debug.Log("그런 군단 이름은 없습니다.");
            return;
        }
        var Army = armies[legionIdx];

        obj.SetOwner(Army);
        Army.AddSolider(obj);

        if (Army.Ability != null)
        {
            //들어왓는데 시너지가 잇다면 스탯추가
            obj.AddStat(Army.Ability);
        }

    }

    /// <summary>
    /// 장군펭귄을 군단에 넣는 함수
    /// </summary>
    /// <param name="legionName">군단 이름</param>
    /// <param name="legionIdx"> 몇번째 군단</param>
    /// <param name="obj"> Penguin 타입만 가능</param>
    public void JoinArmyToGeneral(string legionName, int legionIdx, General obj) //들어가고 싶은 군단, 장군펭귄
    {
        if (armies.Find(p => p.LegionName == legionName) == null)
        {
            Debug.Log("그런 군단 이름은 없습니다.");
            return;
        }

        var Army = armies[legionIdx];

       /* if (Army.General != null)
        {
            Debug.Log($"현재 {legionName}군단에는 장군이 존재합니다.");
            return;
        }*/

        obj.SetOwner(Army);
        Army.AddGeneral(obj);

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
        var Army = armies[penguin.MyArmy.LegionIdx];

        penguin.SetOwner(null);

        //장군이라면
        if (penguin is General)
        {
            var stat = penguin.ReturnGenericStat<GeneralStat>();
            stat.GeneralDetailData.synergy.Stat.OnValidate -= Army.AddStat;
            Army.RemoveStat(Army.Ability);
            Army.Ability = null;

            Army.RemoveGeneral();
        }
        else if (penguin is Penguin)
        {
            //군단 리스트에서 제외
            Army.RemoveSolider(penguin);

            if (Army.Ability != null)
                penguin.RemoveStat(Army.Ability);
        }
    }



    #endregion

    /// <summary>
    /// 펭귄 생성
    /// </summary>
    /// <param name="cloneData"> 펭귄 데이터</param>
    /// <param name="slotIdx"> 위치 인덱스</param>
    /// <returns></returns>
    public Penguin SpawnPenguin(EntityInfoDataSO cloneData, int slotIdx)
    {
        Penguin spawnPenguin = PenguinManager.Instance.SpawnSoldier(cloneData.PenguinType, SpawnPoint.position, _armyPostions[slotIdx]);

        PenguinManager.Instance.AddSoliderPenguin(spawnPenguin);
        PenguinManager.Instance.AddInfoDataMapping(cloneData, spawnPenguin);

        return spawnPenguin;
    }

    /// <summary>
    /// 새로운 군단 생성
    /// </summary>

    public Army CreateArmy(string armyName)
    {
        Army newArmy = new Army();

        newArmy.LegionIdx = armies.Count;
        newArmy.MoveSpeed = 4f;
        newArmy.LegionName = armyName;
        newArmy.IsArmyReady = false;
        newArmy.SynergyType = SynergyType.Police;

        armies.Add(newArmy);
        return newArmy;
    }

    public bool CheckEmpty()
    {
        if (armies.Count <= 0) return true;

        bool result = armies.All(x => x.CheckEmpty());
        return result;
    }


}
