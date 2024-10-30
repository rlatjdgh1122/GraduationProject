using ArmySystem;
using SynergySystem;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ArmyManager : Singleton<ArmyManager>
{
    [SerializeField] private List<Army> _armies = new();
    [SerializeField] private SettingArmyPostion _settingArmyPsotion = null;
    [SerializeField] private Color _selectedOutlineColor = Color.white;

    #region property

    public int CurLegion
    {
        get
        {
            int curLegion = curArmyIdx + 1;

            if (curLegion <= 0) return 1;
            return curLegion;
        }
    }


    public Army CurArmy
    {
        get
        {
            if (_armies.Count <= 0) return null;
            return _armies[curArmyIdx < 0 ? 0 : curArmyIdx];
        }
    }

    public List<Army> Armies { get { return _armies; } }

    #endregion

    private int prevArmyIdx = -1;
    private int curArmyIdx = -1;

    public int ArmiesCount => _armies.Count;


    private Transform SpawnPoint => GameManager.Instance.TentTrm;
    private List<Vector3> _armyPostions = new();

    private SkillInput _skillInput;

    public override void Awake()
    {
        base.Awake();
        //_armyPostions = _settingArmyPsotion.Transforms.Convert(p => p.position);

        _skillInput = GetComponent<SkillInput>();

        _armies.TryClear();
    }
    private void OnEnable()
    {
        SignalHub.OnBattlePhaseStartEvent += OnBattleStart;
        SignalHub.OnBattlePhaseEndEvent += OnBattleEnd;

        if (_settingArmyPsotion != null)
            _armyPostions = _settingArmyPsotion.Transforms.Convert(p => p.position);
    }

    private void OnDisable()
    {
        SignalHub.OnBattlePhaseStartEvent -= OnBattleStart;
        SignalHub.OnBattlePhaseEndEvent -= OnBattleEnd;
    }

    private void OnBattleStart()
    {
        var prevArmy = _armies[prevArmyIdx > 0 ? prevArmyIdx : 0];
        var curArmy = _armies[curArmyIdx > 0 ? curArmyIdx : 0];

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
        if (CurArmy.MovefocusMode == MovefocusMode.MustMove) return;
        CurArmy.MovefocusMode = MovefocusMode.Command;
    }

    public void ChangedMoveFocusMode(Army army, MovefocusMode mode)
    {
        army.MovefocusMode = mode;
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
        if (_armies.Count < legion) return;

        SignalHub.OnModifyCurArmy?.Invoke();

        int Idx = legion - 1;
        var curArmy = _armies[Idx];

        var General = curArmy.General;

        //중복 선택된 군단도 아웃라인 보이게
        curArmy.Soldiers.ForEach(s =>
        {
            s.OutlineCompo.enabled = true;
            s.OutlineCompo.SetColor(_selectedOutlineColor);
            s.HealthCompo.IsAlwaysShowUI = true;
            s.HealthCompo?.OnUIUpdate?.Invoke(s.HealthCompo.currentHealth, s.HealthCompo.maxHealth);
        });

        if (General)
        {
            var GeneralHealtCompo = General.HealthCompo;

            General.OutlineCompo.enabled = true;
            General.OutlineCompo.SetColor(_selectedOutlineColor);
            GeneralHealtCompo.IsAlwaysShowUI = true;
            GeneralHealtCompo?.OnUIUpdate?.Invoke(GeneralHealtCompo.currentHealth, GeneralHealtCompo.maxHealth);
        }

        //군단 체인지 하는건 한 번만 실행해도 되니깐
        //이전과 같은 군단을 선택했다면 리턴
        if (curArmyIdx == Idx) return;

        if (curArmy.CheckEmpty())
        {
            UIManager.Instance.ShowWarningUI($"{curArmy.LegionName}군단에는 펭귄이 존재하지 않습니다.");
            return;
        }
        else if (curArmy.IsHealing)
        {
            UIManager.Instance.ShowWarningUI($"{curArmy.LegionName}군단 회복 중");
        }
        else
            UIManager.Instance.ShowWarningUI($"{curArmy.LegionName}군단 선택");

        prevArmyIdx = curArmyIdx < 0 ? 0 : curArmyIdx;
        curArmyIdx = Idx;

        SignalHub.OnArmyChanged.Invoke(_armies[prevArmyIdx], _armies[curArmyIdx]);

        _armies.IdxExcept
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
        return _armies[legion - 1];
    }

    public Army GetArmyByLegionName(string armyName)
    {
        return _armies.Find(x => x.LegionName == armyName);
    }

    public void SetArmySynergy(int legionIdx, SynergyType synergyType)
    {
        _armies[legionIdx].SynergyType = synergyType;
    }

    public Army GetArmyBySynergyType(SynergyType synergyType)
    {
        Army result = null;
        result = _armies.Find(a => a.SynergyType == synergyType && a.IsSynergy);

        return result;
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
        _armies[curArmyIdx].AddStat(value, type, mode);
    }

    /// <summary>
    /// 현재 선택된 군단의 스탯을 삭제
    /// </summary>
    /// <param name="value"> 삭제할 값(단, 똑같은 값이 존재해야함)</param>
    /// <param name="type"> 값을 삭제할 스탯타입 </param>
    /// <param name="mode"> 상승 또는 감소</param>
    public void RemoveStatCurAmry(int value, StatType type, StatMode mode)
    {
        _armies[curArmyIdx].RemoveStat(value, type, mode);
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
        _armies[legion - 1].RemoveStat(value, type, mode);
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
        _armies[legion - 1].AddStat(value, type, mode);
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
       /* if (_armies.Find(p => p.LegionName == legionName) == null)
        {
            Debug.Log("그런 군단 이름은 없습니다.");
            return;
        }*/
        var Army = _armies[legionIdx];

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
        //var army = _armies.Find(p => p.LegionName == legionName);
        /*if ( == null)
        {
            Debug.Log("그런 군단 이름은 없습니다.");
            return;
        }*/

        var Army = _armies[legionIdx];

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
        var Army = _armies[penguin.MyArmy.LegionIdx];

        //penguin.SetOwner(null);

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

    public Army CreateArmy()
    {
        Army newArmy = new Army();

        newArmy.LegionIdx = _armies.Count;
        newArmy.MoveSpeed = 4f;
        newArmy.LegionName = $"{_armies.Count + 1}";
        newArmy.IsArmyReady = false;
        newArmy.SynergyType = SynergyType.Police;

        _armies.Add(newArmy);
        return newArmy;
    }

    public bool CheckEmpty()
    {
        if (_armies.Count <= 0) return true;

        bool result = _armies.All(x => x.CheckEmpty());
        return result;
    }


}
